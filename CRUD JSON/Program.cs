using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            Crud bewerkingen = new Crud();

            Leerlingen nieuweLijst = new Leerlingen
            {
                LeerlingLijst = new List<Leerling>
                {
                    new Leerling
                    {
                        Naam = "Janssens",
                        Voornaam = "Jan",
                        Geboortedatum = new DateTime(2001, 2, 23),
                        Klas = "5IB",
                        Punten = new List<Punt>
                        {
                            new Punt("Frans", 9.5),
                            new Punt("Wiskunde", 5.5)
                        }
                    },

                    new Leerling
                    {
                        Naam = "Willems",
                        Voornaam = "Wim",
                        Geboortedatum = new DateTime(2001, 2, 23),
                        Klas = "6IB",
                        Punten = new List<Punt>
                        {
                            new Punt("Frans", 10),
                            new Punt("Wiskunde", 8.5)
                        }
                    }
                }
            };

            //Maak een JSON object van het C# object nieuwelijst -> SerializeObject()
            string json = JsonConvert.SerializeObject(nieuweLijst, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();

            //Maak een C# object bestaandelijst van een JSON object -> DeserializeObject()
            Leerlingen bestaandelijst = JsonConvert.DeserializeObject<Leerlingen>(json);
            bestaandelijst.LeerlingLijst[0].Klas = "6IB";

            //Maak terug een JSON object
            json = JsonConvert.SerializeObject(bestaandelijst, Formatting.Indented);
            Console.WriteLine(json);
            Console.ReadLine();


            foreach (Leerling ll in bestaandelijst.LeerlingLijst)
            {
                Console.WriteLine(ll.Naam + " " + ll.Voornaam);
            }
            Console.ReadLine();

            //Create
            //bewerkingen.VoegPuntToe(bestaandelijst, "Willems", "Wim", "NaWe", 7.5);
            //Read
            //bewerkingen.ToonLeerlingen(bestaandelijst);
            //bewerkingen.ToonPunten(bestaandelijst, "Willems", "Wim");
            //Update
            bewerkingen.PasPuntAan(bestaandelijst, "Willems", "Wim", "NaWe", 9.5);
            //Delete
            //bewerkingen.VerwijderPunten(bestaandelijst, "Willems", "Wim", "NaWe");

        }

    }

    public class Leerlingen
    {
        public List<Leerling> LeerlingLijst { get; set; }
    }

    public class Leerling
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Klas { get; set; }
        public List<Punt> Punten { get; set; }
        
    }

    public class Punt
    {
        public string Vak { get; set; }
        public double Punten { get; set; }

        public Punt(string vak, double punten)
        {
            this.Vak = vak;
            this.Punten = punten;
        }
        
    }

    public class Crud
    {
        public void PasPuntAan(Leerlingen lijst, string naam, string voornaam, string vak, double punten)
        {
            foreach (Leerling ll in lijst.LeerlingLijst)
            {
                if (ll.Naam == naam && ll.Voornaam == voornaam)
                {
                    Console.WriteLine("LEERLING: " + naam + " " + voornaam + " GEVONDEN");
                    foreach (Punt p in ll.Punten)
                    {
                        if (p.Vak == vak)
                        {
                            p.Punten = punten;
                            Console.WriteLine("VAK " + vak + " GEVONDEN EN AANGEPAST");
                        }
                    }
                }
            }
            Console.ReadLine(); ;
        }

        public void ToonLeerlingen(Leerlingen bestaandelijst)
        {
            foreach (Leerling ll in bestaandelijst.LeerlingLijst)
            {
                Console.WriteLine(ll.ToString());
            }
            Console.ReadLine();
        }

        public void ToonPunten(List<Leerlingen> lijst, string naam, string voornaam)
        {
            foreach (Leerling ll in lijst)
            {
                if (ll.Naam == naam && ll.Voornaam == voornaam)
                {
                    foreach ( Punt p in lijst)
                    {
                        Console.WriteLine(p);
                    }
                }
            }
        }

        public void VerwijderPunten(Leerlingen lijst, string naam, string voornaam, string vak)
        {
            foreach (Leerling ll in lijst.LeerlingLijst)
            {
                if (ll.Naam == naam && ll.Voornaam == voornaam)
                {
                    foreach (Punt p in ll.Punten)
                    {
                        if (p.Vak == vak)
                        {
                            var itemtoremove = ll.Punten.Single(r => r.Vak == vak);
                            ll.Punten.Remove(itemtoremove);
                        }
                    }
                }

            }
        }

        public void VoegPuntToe(Leerlingen lijst, string v1, string v2, string v3, double v4)
        {
            lijst.LeerlingLijst[0].Punten.Add(new Punt("Informatica", 5));
        }
    }


}
