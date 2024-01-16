using System;
using System.Text;

namespace pizza
{
    class PizzaPersonnalisee : Pizza
    {
        static int nbPizzaPersonnalisee = 0;
        public PizzaPersonnalisee () : base("personnalisée",5,false,null)
        {
            nbPizzaPersonnalisee++;
            nom = "Personnalisée " + nbPizzaPersonnalisee;

            ingredients = new List<string>();
            
            while(true)
            { 
                Console.Write("Rentrer un ingredient pour la pizza personnalisée" + nbPizzaPersonnalisee+ "(ENTER pour terminer) : ");
                var ingredient = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }
                if (ingredients.Contains(ingredient))
                {
                    Console.WriteLine("ERREUR :  cet ingredient est deja present dans la pizza ");
                }
                else
                {
                    ingredients.Add(ingredient);
                    Console.WriteLine(string.Join(",", ingredients));
                    
                }
               
                Console.WriteLine();

            }

            prix = 5 + ingredients.Count * 1.5f;
        }
    }

    class Pizza
    {
        protected string nom;
        public float prix { get; protected set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; protected set; }
        

        public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
        {
            this.nom = nom;
            this.prix = prix;
            this.vegetarienne = vegetarienne;
            this.ingredients = ingredients;
        }

        public void Afficher()
        {
            string badgeVegetarienne = "(V)";

            badgeVegetarienne = vegetarienne ? "(V)" : " ";

            string nomAffiche = FormatPremierlettreMajuscule(nom);

            if (!vegetarienne )
            {
                badgeVegetarienne = " ";
            }
            var ingredientAfficher = ingredients.Select(i => FormatPremierlettreMajuscule(i)).ToList();
            Console.WriteLine(nomAffiche + badgeVegetarienne + " - " + prix + " €");
      
            //var ingredientAfficher = new List<string>();

            /* foreach(var ingredient in  ingredients)
             {
                 ingredientAfficher.Add(FormatPremierlettreMajuscule(ingredient));

             }*/

           
            Console.WriteLine(string.Join(", ", ingredientAfficher));

            Console.WriteLine();

        }
        private static string FormatPremierlettreMajuscule(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return s;
            }

            string minuscules = s.ToLower();
            string majuscules = s.ToUpper();

            string resultat = majuscules[0] + minuscules[1..];

            return resultat;

        }
        public bool ContientIngredient(string ingredient)
        {
            return ingredients.Where(i => i.ToLower().Contains(ingredient)).ToList().Count > 0;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var listePizza = new List<Pizza>()
            { 
                new Pizza("04 fromages", 11.5f, true, new List<string>{"cantal","mozarella","fromage de chevre","gruyere"}),
                new Pizza("indienne", 10.5f, false,new List<string>{"curry","mozarella","poulet","poivron","oignon","coriandre"}),
                new Pizza("mexicaine", 13f, false,new List<string>{"boeuf","mozarella","mais","tomates"}),
                new Pizza("margherita", 8f, true, new List<string>{"sauce tomate","mozarella","fromage de chevre","gruyere"}),
                new Pizza("calzone", 12f, false,new List<string>{"tomate","jambon","mozarella","oignons"}),
                new Pizza("complete", 9.50f, false,new List<string>{ "jambon", "mozarella","oeuf","fromage"}),
                new PizzaPersonnalisee(),
                new PizzaPersonnalisee(),
            };

            //afficher par ordre de prix
            //listePizza = listePizza.OrderBy(p => p.prix).ToList();

            float prixMin = 0;
            float prixMax = 0;

            Pizza pizzaPrixMin = null;
            Pizza pizzaPrixMax = null;

            prixMin = listePizza[0].prix;
            prixMax = listePizza[0].prix;

            pizzaPrixMin = listePizza[0];
            pizzaPrixMax = listePizza[0];

            foreach ( var pizza in listePizza)
            {
                if(pizza.prix  < pizzaPrixMin.prix)
                {
                    pizzaPrixMin = pizza;
                }
                if(pizza.prix > pizzaPrixMax.prix)
                {
                    pizzaPrixMax = pizza;
                }
            }
            /*var pizza1 = new Pizza("04 fromages", 11.5f, true);
            pizza1.Afficher();*/

            //selectionner les pizzas vegetariennes

           /* listePizza = listePizza.Where(p => p.vegetarienne).ToList();

            listePizza = listePizza.Where(p => p.ContientIngredient("tomate")).ToList();
*/
            

            foreach (var pizza in listePizza)
            {
                pizza.Afficher();
            }
            Console.WriteLine();
            Console.WriteLine("la pizza la moins chere est : ");
            pizzaPrixMin.Afficher();
            Console.WriteLine("la pizza la plus chere est : ");
            pizzaPrixMax.Afficher();
        }
    }
}
