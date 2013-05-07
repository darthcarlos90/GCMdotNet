using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PushSharp.Core;
using PushSharp.Android;
using PushSharp;


namespace GCMTest
{
    class Program
    {
        static string mensaje;
        static void Main(string[] args)
        {
            Console.WriteLine("Starting.....");
            Console.WriteLine("Escribe el mensaje que deseas mandar!");
            mensaje = Console.ReadLine();
            send("APA91bENmmPVZ2KYh3Azbrf7riEHAiOHAjkRu - HKohaoj6LJxqE_HWCHW4LF - vwzEhNiaG_TvkS5Ou0F4qZWFxDDDfIXt7kjMyzA8OBqnFFjh85Hk - bk6wHmM81urp3VW9nue7X7uSEByVASZjdepsiKGw7EpHXKhA");
            //method();
            Console.WriteLine("finished");
            Console.ReadLine();     
        }

        static void method()
        {
            var push = new PushBroker();
            push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyCgbcxXlsnATnvFsjxyBkUSARGEjnKXQDo"));
            push.QueueNotification(new GcmNotification().ForDeviceRegistrationId("APA91bENmmPVZ2KYh3Azbrf7riEHAiOHAjkRu - HKohaoj6LJxqE_HWCHW4LF - vwzEhNiaG_TvkS5Ou0F4qZWFxDDDfIXt7kjMyzA8OBqnFFjh85Hk - bk6wHmM81urp3VW9nue7X7uSEByVASZjdepsiKGw7EpHXKhA")
                      .WithJson(@"{""alert"":""Hello World!"",""badge"":7,""sound"":""sound.caf""}"));

            push.StopAllServices();
        }
        
       

        static void send(string regId)
        {
            var applicationID = "AIzaSyCgbcxXlsnATnvFsjxyBkUSARGEjnKXQDo";


            var SENDER_ID = "senderId";
            var value = mensaje;
            
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            // string postData = "{ 'registration_id': [ '" + regId + "' ], 'data': {'message': '" + txtMsg.Text + "'}}";
            string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" + regId;
            Console.WriteLine(postData + "\n");
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            Console.WriteLine(sResponseFromServer + "\n");
            tReader.Close();
            dataStream.Close();
            tResponse.Close();
        }
    }
}
