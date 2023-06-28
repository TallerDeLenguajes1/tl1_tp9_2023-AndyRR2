using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using EspacioClaseJson;
using System.Text.Encodings.Web;

internal class Program{
    
    static void Main(string[] args){
        VerPrecios();
    }

    //https://api.coindesk.com/v1/bpi/currentprice.json
    private static void VerPrecios(){
    var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
    var request = (HttpWebRequest)WebRequest.Create(url);
    request.Method = "GET";
    request.ContentType =  "aplication/json";
    request.Accept =  "aplication/json";
    try{ 
        using (WebResponse response = request.GetResponse())
        {
            using (Stream str = response.GetResponseStream())
            {
                if (str == null) return;
                {
                    using (StreamReader strR = new StreamReader(str))
                    {
                        string responseBody = strR.ReadToEnd();
                        CambiosBTC moneda = JsonSerializer.Deserialize<CambiosBTC>(responseBody);//creo objeto moneda de CambiosBTC deserializado
                        //Console.WriteLine("Moneda: " + moneda[0].bpi.USD.symbol + "Precio: " + moneda[0].bpi.USD.rate_float);
                        Console.WriteLine("Moneda: " + moneda.bpi.USD.code + " " + "Precio: " + moneda.bpi.USD.rate_float);
                        Console.WriteLine("Moneda: " + moneda.bpi.EUR.code + " " + "Precio: " + moneda.bpi.EUR.rate_float);
                        Console.WriteLine("Moneda: " + moneda.bpi.GBP.code + " " + "Precio: " + moneda.bpi.GBP.rate_float);
                    }
                }
            }
        }
    }
    catch (WebException)
    {
        Console.WriteLine("Problemas en el acceso a la API");
    }
}

}



