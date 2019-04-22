using Control_22_04.Models;
using Control_22_04.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_22_04
{
    public class NomadPublisher
    {
        private List<Order> orders;
        private List<Journal> journals;
        private List<User> users;

        private PasswordWriterService passwordWriter;
        private RegisterService register;
        private LoginService loginer;

        private AppContext context;

        public void Run()
        {
            passwordWriter = new PasswordWriterService();
            register = new RegisterService();
            loginer = new LoginService();
            using (var context = new AppContext())
            {
                orders = context.Orders.ToList();
                journals = context.Journals.ToList();
                for (int i = 0; i < context.Users.ToList().Count; i++)
                {
                    if (context.Users.ToList()[i].Subscription != null && context.Users.ToList()[i].Subscription.EndDate < DateTime.Now)
                    {
                        context.Users.ToList()[i].Subscription = null;
                        context.SaveChanges();
                    }
                }
                users = context.Users.ToList();
            }
            

            while (MainMenu())
            {

            }
        }
        private bool MainMenu()
        {
            int mainMenu;
            Console.WriteLine("Nomad Kazakhstan Journal\n ");

            Console.WriteLine("Выберите действие: ");
            Console.WriteLine("1) Регистрация ");
            Console.WriteLine("2) Вход ");
            Console.WriteLine("3) Выйти из приложения ");
            if(int.TryParse(Console.ReadLine(), out mainMenu))
            {
                if(mainMenu == 1)
                {
                    User newUser;
                    if(register.TryAddUser(users, out newUser))
                    {
                        users.Add(newUser);
                        using (context = new AppContext())
                        {
                            context.Users.Add(newUser);
                            context.SaveChanges();
                        }
                        Console.WriteLine("Регистрация прошла успешно!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Регистрация прервана!");
                    }
                }
                else if(mainMenu == 2)
                {
                    string userLogin, userPassword;
                    Console.WriteLine("Введите логин : ");
                    userLogin = Console.ReadLine();
                    Console.WriteLine("Введите пароль: ");
                    userPassword = passwordWriter.Write();
                    if(loginer.Access(users, userLogin, userPassword))
                    {
                        InnerMenu(userLogin);
                    }
                    else
                    {
                        Console.WriteLine("Логин или пароль неверны!");
                        return true;
                    }
                    
                }
                else if(mainMenu == 3)
                {
                    Environment.Exit(0);
                }
            }
            

            return true;
        }
        private void InnerMenu(string userLogin)
        {
            int innerMenu, subscriptionIndex;
            int loginnedUserIndex = -1;
            for(int i = 0; i < users.Count; i++)
            {
                if (users[i].Login.Equals(userLogin))
                {
                    loginnedUserIndex = i;
                    break;
                }
            }
            while (true)
            {
                if (users[loginnedUserIndex].Subscription == null)
                {
                    Console.WriteLine("Выберите действие: ");
                    Console.WriteLine("1) Оформить подписку");
                    Console.WriteLine("2) Выйти");

                    Subscription subscription = new Subscription();

                    if (int.TryParse(Console.ReadLine(), out innerMenu))
                    {
                        if (innerMenu == 1)
                        {
                            Console.WriteLine("Выберите версию");
                            Console.WriteLine($"1) 12 мес. 14000тг");
                            Console.WriteLine($"2) 24 мес. 24000тг");
                            Console.WriteLine($"3) 36 мес. 32000тг");

                            if (int.TryParse(Console.ReadLine(), out subscriptionIndex))
                            {
                                switch (subscriptionIndex)
                                {
                                    case 1:
                                        Console.WriteLine($"К оплате 14000тгтг");
                                        subscription.StartDate = DateTime.Now;
                                        subscription.EndDate = subscription.StartDate.AddMonths(12);
                                        subscription.SubscriptionVersion = SubscriptionVersion.Twelve;
                                        break;
                                    case 2:
                                        Console.WriteLine($"К оплате 24000тгтг");
                                        subscription.StartDate = DateTime.Now;
                                        subscription.EndDate = subscription.StartDate.AddMonths(24);
                                        subscription.SubscriptionVersion = SubscriptionVersion.TwentyFour;
                                        break;
                                    case 3:
                                        Console.WriteLine($"К оплате 32000тг");
                                        subscription.StartDate = DateTime.Now;
                                        subscription.EndDate = subscription.StartDate.AddMonths(36);
                                        subscription.SubscriptionVersion = SubscriptionVersion.ThirtySix;
                                        break;
                                    default:
                                        Console.WriteLine("Такой подписки нет!");
                                        break;
                                }
                                using (var context = new AppContext())
                                {
                                    context.Users.Remove(context.Users.ToList()[loginnedUserIndex]);
                                    users[loginnedUserIndex].Subscription = subscription;
                                    context.Users.Add(users[loginnedUserIndex]);
                                    context.SaveChanges();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректный ввод!");
                            }


                        }
                        else if (innerMenu == 2)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Выберите действие: ");
                    Console.WriteLine("1) Заказать журнал: ");
                    Console.WriteLine("2) Отменить подписку");
                    Console.WriteLine("3) Выйти");

                    if (int.TryParse(Console.ReadLine(), out innerMenu))
                    {
                        if(innerMenu == 3)
                        {
                            return;
                        }
                        else if(innerMenu == 1)
                        {       
                            Console.WriteLine("Вы заказали журнал!");
                        }
                        else if(innerMenu == 2)
                        {
                            users[loginnedUserIndex].Subscription = null;
                            using (context = new AppContext())
                            {
                                context.Users.Remove(context.Users.ToList()[loginnedUserIndex]);
                                context.Users.Add(users[loginnedUserIndex]);
                                context.SaveChanges();
                            }
                            Console.WriteLine("Мы сожалеем, что Вам не понравился наш журнал");
                        }
                    }
                }
            }
        }
    }
}
