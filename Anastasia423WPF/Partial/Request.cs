using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Anastasia423WPF
{
    public partial class requestUnFreeze
    {
        public string TargetName
        {
            get
            {
                if (bookID != null)
                {
                    return book != null ? $"Книга: {book.Name}" : $"Книга (ID: {bookID})";
                }
                return user_ != null ? $"Аккаунт: {user_.Login}" : "Неизвестный объект";
            }
        }
    }
    public partial class requestRole
    {
        public string UserName => user_ != null ? user_.Login : "Нет данных";
    }

}
