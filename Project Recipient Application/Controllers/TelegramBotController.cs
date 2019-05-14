using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Project_Recipient_Application.Utility;
using Project_Recipient_Application.Models;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace Project_Recipient_Application.Controllers
{
    public class TelegramBotController : ApiController
    {

        TelegramBotClient bot;
        private static List<ModelList> list = new List<ModelList>();
        public TelegramBotController()
        {
            bot = new TelegramBotClient(BotToken.Api);
        }

        [HttpPost]
        public async Task<bool> Main(Update update)
        {
            #region get update
            try
            {
                if (update.Type == UpdateType.Message && update.Message != null)
                {
                    await JoinMember(update);
                }
            }
            catch (Exception ex)
            {
                await bot.SendTextMessageAsync(update.Message.Chat.Id, "ربات فعال نمی باشد در حال به روز رسانی  ", ParseMode.Default, false, false, 0);
                await bot.SendTextMessageAsync("408129494", "فرداد مشکلی پیش اومده :" + ex.ToString(), ParseMode.Default, false, false, 0);
            }
            return true;
            #endregion
        }

        #region JOIN MEMBER
        private async Task<bool> JoinMember(Update update)
        {
            var db = new DatabaseContext();
            if (update.Message.Text != null)
            {
                ModelList index = new ModelList();
                if (chackindex(update.Message.From.Id))
                {
                    index = GetIndex(update.Message.From.Id);
                    if (update.Message.Text == "⛔️انصراف⛔️")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "ارسال پروژه  شما لغو شد ", ParseMode.Default, false, false, 0, KeyBot.Main());
                        Delete(update.Message.From.Id);
                        return true;
                    }
                    if (index.step == "s0")
                    {
                        string nums = ToEnglishNumber(update.Message.Text.Trim());
                        if (Regex.IsMatch(nums, @"[^0-9]"))
                        {
                            index.step = "s0";
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌شماره دانشجویی وارد شده معتبر نیست لطفا مجدد شماره دانشجویی خود را ارسال کنید❌", ParseMode.Default, false, false, 0, KeyBot.cancell());
                            Delete(update.Message.From.Id);
                            return true;
                        }
                        else
                        {
                            if (Checkdb(nums) && nums.ToString().Length == 14)
                            {
                                index.step = "s1";
                                index.stucode = nums.ToString().Trim();
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "موضوع پروژه خود را بنویسید", ParseMode.Default, false, false, 0, KeyBot.cancell());
                            }
                            else
                            {
                                index.step = "s0";
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌شماره دانشجویی وارد شده معتبر نیست  لطفا مجدد  تلاش کنید❌", ParseMode.Default, false, false, 0, KeyBot.cancell());
                            }

                        }
                    }
                    else if (index.step == "s1")
                    {
                        if (update.Message.Text.Trim().Length < 49)
                        {
                            index.step = "s2";
                            index.Title = update.Message.Text.Trim();
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "ماهیت پروژه خود را انتخاب کنید", ParseMode.Default, false, false, 0, KeyBot.SortPro());
                        }
                        else
                        {
                            index.step = "s1";
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌ عنوانی که ارسال کرده اید از 50 حرف بیشتر شده است و مجاز برای ثبت نیست ، عنوان خود را مجدد ارسال کنید❌", ParseMode.Default, false, false, 0, KeyBot.cancell());
                        }
                    }
                    else if (index.step == "s2")
                    {
                        if (update.Message.Text.Trim() == "پروژه" || update.Message.Text.Trim() == "تحقیق")
                        {
                            index.sortFile = update.Message.Text.Trim();
                            index.step = "s3";
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "✅لطفا فایل خود را ارسال کنید✅", ParseMode.Default, false, false, 0, KeyBot.cancell());
                        }
                        else
                        {
                            index.step = "s2";
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "لطفا ماهیت فایل خود را به درستی با دکمه های زیر مشخص کنید", ParseMode.Default, false, false, 0, KeyBot.SortPro());
                        }
                    }
                    else if (index.step == "s3")
                    {
                        if (update.Message.Type == MessageType.Document)
                        {
                            if (update.Message.Document.FileSize < 2000000 && (update.Message.Document.FileName.Contains("zip")))
                            {
                                string respath = await DownloadFile(update.Message.Document.FileId);
                                if (respath != "false")
                                {
                                    index.step = "se";
                                    index.UrlFile = respath;
                                    index.Date = DateTime.Now;
                                }
                                else
                                {
                                    index.step = "s3";
                                    await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌خطایی  در دریافت رخ داده است لطفا مجدد فایل خود را ارسال کنید❌", ParseMode.Default, false, false, 0, KeyBot.cancell());
                                }

                            }
                            else
                            {
                                index.step = "s3";
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌نوع فایل شما مناسب نیست فایل شما باید ZIP باشد و یا حجم فایل شما کمتر از 20MB است ❌", ParseMode.Default, false, false, 0, KeyBot.cancell());
                            }
                        }
                        else
                        {
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌لطفا فایل خود را به صورت ZIP ارسال کنید ❌", ParseMode.Default, false, false, 0, KeyBot.cancell());
                        }

                    }


                    if (index.step == "se")
                    {
                        try
                        {
                            Project project = new Project()
                            {
                            
                                DateSend = index.Date,
                                sortFile = index.sortFile,
                                Title = index.Title,
                                Urlfile = index.UrlFile,
                                SturId = db.students.FirstOrDefault(z => z.stuCode == index.stucode).ID,
                                
                            };
                            db.projects.Add(project);

                            if (Convert.ToBoolean(db.SaveChanges()) == true)
                            {
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "پروژه شما با موفقیت دریافت شد☑️ ", ParseMode.Default, false, false, 0, KeyBot.Main());
                                Delete(update.Message.From.Id);
                                return true;
                            }
                            else
                            {
                                await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌ ارسال پروژه شما شما با موفقیت انجام نشد ❌", ParseMode.Default, false, false, 0, KeyBot.Main());
                                Delete(update.Message.From.Id);
                                return true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Delete(update.Message.From.Id);
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, "❌عملیات ثبت پروژه شما شکست روبرو شد ممکن است شما بیش از حد مجاز پروژه ثبت کرده اید ❌", ParseMode.Default, false, false, 0, KeyBot.Main());
                            await bot.SendTextMessageAsync("408129494", "مشکلی پیش به شرح زیر " + ex.Message.ToString() + "  " + ex.Data, ParseMode.Default, false, false, 0, KeyBot.Main());
                            return true;
                        }
                    }
                }
                else
                {
                    if (update.Message.Text == "ارسال پروژه")
                    {
                        list.Add(new ModelList
                        {
                            chatID = update.Message.From.Id.ToString(),
                            step = "s0"
                        });
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "🔖کد دانشجویی خود را ارسال کنید🔖", ParseMode.Default, false, false, 0, KeyBot.cancell());
                    }
                    else if (update.Message.Text == "درباره ما")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "این ربات به شما کمک می کند تا کارمندان شما در این قسمت ثبت نام کنند و شما اطلاعات آن ها را به سادگی جست و جو کنید .سازنده: فرداد جعفری شماره تماس: 09029991355", ParseMode.Default, false, false, 0, KeyBot.Main());
                    }
                    else if (update.Message.Text == "سلام" || update.Message.Text == "//start" || update.Message.Text == "راه اندازی مجدد")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "🌸به بات  دریافت پروژه دانشکده شمسی پور خوش آمدید🌸", ParseMode.Default, false, false, 0, KeyBot.Main());
                    }

                    else if (update.Message.Text == "⛔️خروج⛔️")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "//stop", ParseMode.Default, false, false, 0, KeyBot.Main());
                    }
                    else if (update.Message.Text == "برسی")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "📵هنوز این قابلیت فعال نشده است📵", ParseMode.Default, false, false, 0, KeyBot.Main());
                    }
                    else if (update.Message.Text == "iamadmin")
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "این ویژگی هنوز فعال نشده است  ", ParseMode.Default, false, false, 0, KeyBot.Main());
                    }
                    else if (update.Message.Text == "💻 گروه طراح بات 💻")
                    {
                        //Stream file = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath("~//IMG//")) + "PULSE.jpg", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                        //await bot.SendPhotoAsync(update.Message.Chat.Id, new FileToSend("PULSE.jpg", file), "طراحی وبسایت های شرکتی " + "\n\r" + "طراحی اتوماسیون های اداری " + "\n\r" + "طراحی بات های تلگرامی نوبت دهی و شرکتی" + "\n\r" + "مدیریت شبکه های مجازی" + "\n\r" + "مدیریت محتوا و سئو سایت " + "\n\r" + "www.pulse-tm.ir" + "\n\r" + "@pulse_tm" + "\n\r" + "09029991355", false, 0);
                    }
                    else if (update.Message.Text != null)
                    {
                        await bot.SendTextMessageAsync(update.Message.Chat.Id, "❗️دستور وارد شده نامفهوم لطفا بیشتر دقت کن و از صفحه کلید ربات استفاده کن❗️", ParseMode.Default, false, false, 0, KeyBot.Main());
                    }
                }
            }
            return false;
        }
        #endregion

        #region checkindex
        private bool chackindex(int id)
        {
            var user = list.FirstOrDefault(z => z.chatID == id.ToString().Trim());
            if (user != null)
                return true;

            return false;
        }
        #endregion

        #region get index
        private ModelList GetIndex(int Id)
        {

            var user = list.FirstOrDefault(x => x.chatID == Id.ToString());

            return user;
        }
        #endregion

        #region convert to english digite
        private string ToEnglishNumber(string strNum)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < strNum.Length; i++)
            {
                switch (strNum[i])
                {
                    //Persian digits
                    case '\u06f0':
                        sb.Append('0');
                        break;
                    case '\u06f1':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                        sb.Append('9');
                        break;

                    //Arabic digits    
                    case '\u0660':
                        sb.Append('0');
                        break;
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u0669':
                        sb.Append('9');
                        break;
                    default:
                        sb.Append(strNum[i]);
                        break;
                }
            }
            return sb.ToString();
        }
        #endregion

        #region delete index
        private void Delete(int id)
        {
            var delete = list.FirstOrDefault(z => z.chatID == id.ToString());
            if (delete != null)
            {
                list.Remove(delete);
            }
        }
        #endregion

        #region Check Database
        private bool Checkdb(string stuc)
        {
            try
            {
                DatabaseContext db = new DatabaseContext();
                if (db.students.Any(a => a.stuCode == stuc))
                {
                    db.Dispose();
                    return false;
                }
                else
                {
                    db.Dispose();
                    return true;
                }
            }
            catch 
            {

                return false;
            }
          
        }
        #endregion

        #region DownloadFile
        private async Task<string> DownloadFile(string fileId)
        {
            try
            {
                var test = await bot.GetFileAsync(fileId);
                var download_url = @"https://api.telegram.org//file//bot863157478:AAFAB1IyGpEiINCQybbI6jz4M2MpK7uS-Nk//" + test.FilePath;
                string finalyPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~//Documents//")) + string.Format(@"{0}.zip", DateTime.Now.Ticks);
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(download_url), finalyPath);
                }
                return finalyPath;
            }
            catch
            {
                return "false";
            }
        }
        #endregion
    }
}