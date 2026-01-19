using System;

namespace SmartHomeApp
{
    public interface ISmartHomeControl
    {
        void TurnOnLights();
        void SetTemperature(int celsius);
    }

    public class SmsGateway
    {
        public void SendSms(string phone, string text)
            => Console.WriteLine($"[SMS до {phone}]: {text}");
    }

    public class SmsAdapter : ISmartHomeControl
    {
        private readonly SmsGateway _gateway;
        private readonly string _phone;

        public SmsAdapter(SmsGateway gateway, string phone)
        {
            _gateway = gateway;
            _phone = phone;
        }

        public void TurnOnLights() => _gateway.SendSms(_phone, "LIGHTS_ON");

        public void SetTemperature(int t) => _gateway.SendSms(_phone, $"TEMP_{t}");
    }

    class Program
    {
        static void Main()
        {
            ISmartHomeControl home = new SmsAdapter(new SmsGateway(), "+380001112233");

            while (true)
            {
                Console.WriteLine("\n1 - Увімкнути світло, 2 - Встановити 25°C, 0 - Вихід");
                string choice = Console.ReadLine();

                if (choice == "1") home.TurnOnLights();
                else if (choice == "2") home.SetTemperature(25);
                else if (choice == "0") break;
                else Console.WriteLine("Невідома команда");
            }
        }
    }
}