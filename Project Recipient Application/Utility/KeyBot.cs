using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot.Types.ReplyMarkups;

namespace Project_Recipient_Application.Utility
{
    public class KeyBot
    {
        private static KeyboardButton keys(string txt)
        {
            KeyboardButton kb = new KeyboardButton("");
            kb.Text = txt;
            return kb;
        }
        public static ReplyKeyboardMarkup Main()
        {
            ReplyKeyboardMarkup rpl = new ReplyKeyboardMarkup();

            KeyboardButton[] r1 = new KeyboardButton[] { keys("ارسال پروژه") };
            KeyboardButton[] r2 = new KeyboardButton[] { keys("⛔️خروج⛔️") };

            rpl.Keyboard = new KeyboardButton[][] { r1, r2 };

            return rpl;
        }
        public static ReplyKeyboardMarkup cancell()
        {
            ReplyKeyboardMarkup rpl2 = new ReplyKeyboardMarkup();

            KeyboardButton[] r3 = new KeyboardButton[] { keys("⛔️انصراف⛔️") };
            rpl2.Keyboard = new KeyboardButton[][] { r3 };
            return rpl2;
        }

        public static ReplyKeyboardMarkup SortPro()
        {
            ReplyKeyboardMarkup rpl = new ReplyKeyboardMarkup();

            KeyboardButton[] r1 = new KeyboardButton[] { keys("پروژه") };
            KeyboardButton[] r2 = new KeyboardButton[] { keys("مقاله") };

            rpl.Keyboard = new KeyboardButton[][] { r1, r2 };

            return rpl;
        }
    }
}