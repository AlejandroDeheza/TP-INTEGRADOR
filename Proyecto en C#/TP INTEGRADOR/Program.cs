using System;
using System.Collections.Generic;
using System.Linq;

namespace TP_INTEGRADOR 
{
    class Program
    {
        static void Main(string[] args)
        {
            //////////////////////////////////// TEMPORADA 1 /////////////////////////////////////////////////
            EnfermedadInfecciosa malaria = new EnfermedadInfecciosa(500, "Malaria");
            EnfermedadInfecciosa otitis = new EnfermedadInfecciosa(100, "Otitis");
            EnfermedadAutoinmune lupus = new EnfermedadAutoinmune(10000, "Lupus");

            EnfermedadInfecciosa otraMalaria = new EnfermedadInfecciosa(800, "OtraMalaria");

            HashSet<Enfermedad> enfermedadesLogan = new HashSet<Enfermedad>();
            enfermedadesLogan.Add(malaria);
            enfermedadesLogan.Add(otitis);
            enfermedadesLogan.Add(lupus);

            Persona logan = new Persona(36, 3000000, enfermedadesLogan);
            Persona frank = new Persona(36, 3500000, new HashSet<Enfermedad>());

            Console.WriteLine("TEMPORADA 1 \n");

            //////////////////////// PUNTO 1 ////////////////////////
            Console.WriteLine("Punto 1");
            frank.contraerEnfermedad(otraMalaria);
            Console.WriteLine("Frank contrae OtraMalaria");
            Console.WriteLine("Frank tiene OtraMalaria?: " + frank.Enfermedades.Contains(otraMalaria) + "\n");

            //////////////////////// PUNTO 2 ////////////////////////
            Console.WriteLine("Punto 2");
            Console.WriteLine("Celulas amenazadas por Malaria: " + malaria.CantidadCelulasQueAmenaza);
            Console.WriteLine("Malaria se reproduce");
            malaria.reproducirse();
            Console.WriteLine("Celulas amenazadas por Malaria: " + malaria.CantidadCelulasQueAmenaza + "\n");

            //////////////////////// PUNTO 3 ////////////////////////
            Console.WriteLine("Punto 3");
            Console.WriteLine("ESTADO LOGAN");
            Console.WriteLine("Cantidad de celulas: " + logan.CantidadCelulas);
            Console.WriteLine("Temperatura: " + logan.TEMPERATURA);
            Console.WriteLine("Dias que vive: " + logan.DiasQueVivio + "\n");
            logan.vivirUnDia();
            Console.WriteLine("LOGAN VIVE UN DIA \n");
            Console.WriteLine("ESTADO LOGAN");
            Console.WriteLine("Cantidad de celulas: " + logan.CantidadCelulas);
            Console.WriteLine("Temperatura: " + logan.TEMPERATURA);
            Console.WriteLine("Dias que vive: " + logan.DiasQueVivio);
            Console.WriteLine("Cantidad de celulas afectadas por enfermedades agresivas: " + logan.celulasAfectadasPorEnfermedadesAgresivas());
            Console.WriteLine("Enfermedad que mas celulas afecta: " + logan.Enfermedades.OrderBy(e => e.CantidadCelulasQueAmenaza).Last().Nombre);
            Console.WriteLine("Logan esta en coma? " + logan.estaEnComa() + "\n");

            //////////////////////// PUNTO 4 ////////////////////////
            Console.WriteLine("Punto 4");
            Console.WriteLine("LOGAN VIVE 31 DIAS \n");
            logan.vivirNDias(30); // YA HABIA VIVIDO 1 DIA, ASI QUE AHORA LE DIGO QUE VIVA 30 DIAS MAS
            Console.WriteLine("ESTADO LOGAN");
            Console.WriteLine("Cantidad de celulas: " + logan.CantidadCelulas);
            Console.WriteLine("Temperatura: " + logan.TEMPERATURA);
            Console.WriteLine("Dias que vive: " + logan.DiasQueVivio);
            Console.WriteLine("Cantidad de celulas afectadas por enfermedades agresivas: " + logan.celulasAfectadasPorEnfermedadesAgresivas());
            Console.WriteLine("Enfermedad que mas celulas afecta: " + logan.Enfermedades.OrderBy(e => e.CantidadCelulasQueAmenaza).Last().Nombre);
            Console.WriteLine("Logan esta en coma? " + logan.estaEnComa() + "\n");

            //////////////////////////////////// TEMPORADA 2 /////////////////////////////////////////////////
            Medico cameron = new Medico(100, 36, 4000000, new HashSet<Enfermedad>());
            HashSet<Medico> subordinadosHouse = new HashSet<Medico>();
            subordinadosHouse.Add(cameron);
            Medico house = new JefeDeDepartamento(subordinadosHouse, 100, 36, 4000000, new HashSet<Enfermedad>());
            Muerte muerte = new Muerte();


            Console.WriteLine("TEMPORADA 2 \n");

            //////////////////////// PUNTO 1 ////////////////////////
            Console.WriteLine("Punto 1");
            Console.WriteLine("Cantidad de celulas que amenaza la Malaria: " + malaria.CantidadCelulasQueAmenaza);
            Console.WriteLine("Cantidad de celulas que amenaza el Lupus: " + lupus.CantidadCelulasQueAmenaza);
            Console.WriteLine("Se atenuan la Malaria y el Lupus");
            malaria.atenuar(100);
            lupus.atenuar(500);
            Console.WriteLine("Cantidad de celulas que amenaza la Malaria: " + malaria.CantidadCelulasQueAmenaza);
            Console.WriteLine("Cantidad de celulas que amenaza el Lupus: " + lupus.CantidadCelulasQueAmenaza + "\n");

            //////////////////////// PUNTO 2 ////////////////////////
            Console.WriteLine("Punto 2");
            Console.WriteLine("Cameron atiende a Logan");
            cameron.atenderEnfermo(logan);
            Console.WriteLine("Enfermedades que tiene Logan: " + logan.Enfermedades.Count());
            Console.WriteLine("Esta enfermo de " + logan.Enfermedades.First().Nombre + "\n");

            //////////////////////// PUNTO 3 ////////////////////////
            Console.WriteLine("Punto 3");
            Console.WriteLine("Cameron contrae otitis");
            cameron.contraerEnfermedad(new EnfermedadInfecciosa(2000, "Otitis"));
            Console.WriteLine("Enfermedades que tiene Cameron " + cameron.Enfermedades.Count());
            Console.WriteLine("House atiende a Cameron");
            house.atenderEnfermo(cameron);
            Console.WriteLine("Enfermedades que tiene Cameron " + cameron.Enfermedades.Count() + "\n");

            //////////////////////// PUNTO 4 ////////////////////////
            Console.WriteLine("Punto 4");
            Console.WriteLine("Cameron contrae Malaria");
            EnfermedadInfecciosa malaria2 = new EnfermedadInfecciosa(500, "Malaria");
            EnfermedadInfecciosa malaria3 = new EnfermedadInfecciosa(500, "Malaria");
            cameron.contraerEnfermedad(malaria2); // CONTRAE Y SE CURA SOLA
            Console.WriteLine("Cameron tiene Malaria? " + cameron.Enfermedades.Contains(malaria2));
            Console.WriteLine("House contrae Malaria");
            house.contraerEnfermedad(malaria3);  // CONTRAE Y SE CURA SOLO
            Console.WriteLine("House tiene Malaria? " + house.Enfermedades.Contains(malaria3) + "\n");

            //////////////////////// PUNTO 5 ////////////////////////
            Console.WriteLine("Punto 5");
            Console.WriteLine("House contrae la muerte");
            house.contraerEnfermedad(muerte);
            Console.WriteLine("House esta muerto? " + house.estaMuerto());
            Console.WriteLine("House temperatura: " + house.TEMPERATURA);

        }
    }



    //////////////////////////////////////////////// PERSONA ////////////////////////////////////////////////////
    class Persona
    {

        private int temperatura;
        private int cantidadCelulas;
        private int diasQueVivio = 0;
        private HashSet<Enfermedad> enfermedades = new HashSet<Enfermedad>();

        public Persona(int unaTemperatura, int cantCelulas, HashSet<Enfermedad> unasEnfermedades)
        {
            temperatura = unaTemperatura;
            cantidadCelulas = cantCelulas;
            enfermedades = unasEnfermedades;
        }

        public int TEMPERATURA
        {
            get => temperatura;
            set => temperatura = value;
        }

        public int CantidadCelulas
        {
            get => cantidadCelulas;
            set => cantidadCelulas = value;
        }

        public int DiasQueVivio // CREO QUE HAY QUE HACERLO EN ENFERMEDAD, PORQUE SE CUENTA LOS DIAS DE LA ENFERMEDAD
        {
            get => diasQueVivio;
            set => diasQueVivio = value;
        }

        public HashSet<Enfermedad> Enfermedades
        {
            get => enfermedades;
        }

        public virtual void contraerEnfermedad(Enfermedad enfermedad)
        {
            enfermedades.Add(enfermedad);
            if (enfermedad.Nombre == "Muerte")
            {
                enfermedad.afectarPersona(this);
            }
        }

        public bool estaSano()
        {
            return enfermedades.Count == 0;
        }

        public void vivirUnDia()
        {
            diasQueVivio++;
            if (!this.estaSano())
            {
                foreach (Enfermedad enfermedad in enfermedades)
                {
                    enfermedad.afectarPersona(this);
                }
            }

        }

        public bool estaEnComa()
        {
            return temperatura >= 45 || cantidadCelulas < 1000000;
        }

        public int celulasAfectadasPorEnfermedadesAgresivas()
        {
            int cantidad = 0;

            foreach (Enfermedad enfermedad in enfermedades)
            {
                if (enfermedad.esAgresivaPara(this))
                {
                    cantidad += enfermedad.CantidadCelulasQueAmenaza;
                }
            }

            return cantidad;
        }

        public void vivirNDias(int dias)
        {
            Enumerable.Range(0, dias).ToList().ForEach(argumento => this.vivirUnDia());
        }

        public void recibirDosis(int dosis)
        {
            if (!this.estaMuerto())
            {
                foreach (Enfermedad enfermedad in enfermedades)
                {
                    enfermedad.atenuar(dosis * 15);
                }
                this.eliminarEnfermedadesCuradas();
            }
        }

        private void eliminarEnfermedadesCuradas()
        {
            List<Enfermedad> listaEnfermedades = enfermedades.ToList<Enfermedad>();
            foreach (Enfermedad e in listaEnfermedades)
            {
                if (e.CantidadCelulasQueAmenaza <= 0)
                {
                    enfermedades.Remove(e);
                }
            }

        }

        public bool estaMuerto()
        {
            HashSet<String> nombreEnfermedades = Enfermedades.Select(e => e.Nombre).ToHashSet();

            if (nombreEnfermedades.Contains("Muerte"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    //////////////////////////////////////////////// ENFERMEDADES ////////////////////////////////////////////////////
    abstract class Enfermedad
    {

        private int cantidadCelulasQueAmenaza;
        private String nombre;

        public abstract void afectarPersona(Persona unaPersona);
        public abstract bool esAgresivaPara(Persona unaPersona);

        public int CantidadCelulasQueAmenaza
        {
            get => cantidadCelulasQueAmenaza;
            set => cantidadCelulasQueAmenaza = value;
        }

        public String Nombre
        {
            get => nombre;
            set => nombre = value;
        }

        public virtual void atenuar(int atenuacion)
        {
            cantidadCelulasQueAmenaza -= atenuacion;
        }

    }

    class EnfermedadInfecciosa : Enfermedad
    {
        public EnfermedadInfecciosa(int celulasQueAmenaza, String elNombre)
        {
            CantidadCelulasQueAmenaza = celulasQueAmenaza;
            Nombre = elNombre;
        }
        public override void afectarPersona(Persona unaPersona)
        {
            if (unaPersona.TEMPERATURA < 45)
            {
                this.aumentarTemperatura(unaPersona);
            }
        }

        public void aumentarTemperatura(Persona unaPersona)
        {
            if (unaPersona.TEMPERATURA < 45)
            {
                unaPersona.TEMPERATURA += CantidadCelulasQueAmenaza / 1000;
                if (unaPersona.TEMPERATURA > 45)
                {
                    unaPersona.TEMPERATURA = 45; // DE ESTA MANERA LA TEMPERATURA NUNCA PASA LOS 45 GRADOS
                }
            }
        }

        public void reproducirse()
        {
            CantidadCelulasQueAmenaza *= 2;
        }

        public override bool esAgresivaPara(Persona unaPersona)
        {
            return CantidadCelulasQueAmenaza > unaPersona.CantidadCelulas;
        }

    }

    class EnfermedadAutoinmune : Enfermedad
    {
        public EnfermedadAutoinmune(int celulasQueAmenaza, String elNombre)
        {
            CantidadCelulasQueAmenaza = celulasQueAmenaza;
            Nombre = elNombre;
        }

        public override void afectarPersona(Persona unaPersona)
        {
            this.destruirCelulas(unaPersona);
        }

        public void destruirCelulas(Persona unaPersona)
        {
            unaPersona.CantidadCelulas -= CantidadCelulasQueAmenaza;
        }

        public override bool esAgresivaPara(Persona unaPersona)
        {
            return unaPersona.DiasQueVivio > 30;
        }

    }

    class Muerte : Enfermedad
    {
        public Muerte()
        {
            CantidadCelulasQueAmenaza = 0;
            Nombre = "Muerte";
        }

        public override bool esAgresivaPara(Persona unaPersona) => true;
        public override void afectarPersona(Persona unaPersona)
        {
            this.disminuirTemperatura(unaPersona);
        }

        private void disminuirTemperatura(Persona unaPersona)
        {
            unaPersona.TEMPERATURA = 0;
        }

        public override void atenuar(int atenuacion)
        {
            // NO HACE NADA
        }

    }

    //////////////////////////////////////////////// MEDICOS ////////////////////////////////////////////////////
    class Medico : Persona
    {
        int cantidadDosis;

        public Medico(int dosis, int temperatura, int celulas, HashSet<Enfermedad> enfermedades) : base(temperatura, celulas, enfermedades)
        {
            cantidadDosis = dosis;
        }

        public int Dosis
        {
            get => cantidadDosis;
            set => cantidadDosis = value;
        }

        public virtual void atenderEnfermo(Persona unaPersona)
        {
            if (!unaPersona.estaSano())
            {
                this.darDosis(unaPersona);
            }
            else
            {
                throw new EnfermoException(); //FALTA QUE IMPRIMA MENSAJE
            }
        }

        public void darDosis(Persona unaPersona)
        {
            unaPersona.recibirDosis(cantidadDosis);
        }

        public override void contraerEnfermedad(Enfermedad enfermedad)
        {
            base.contraerEnfermedad(enfermedad);
            this.atenderEnfermo(this);
        }

    }

    class JefeDeDepartamento : Medico
    {
        private HashSet<Medico> subordinados;

        public JefeDeDepartamento(HashSet<Medico> losSubordinados, int dosis, int temperatura, int celulas,
            HashSet<Enfermedad> enfermedades) : base(dosis, temperatura, celulas, enfermedades)
        {
            subordinados = losSubordinados;
        }

        public HashSet<Medico> Subordinados
        {
            get => subordinados;
        }

        public override void atenderEnfermo(Persona unaPersona)
        {
            if (!unaPersona.estaSano())
            {
                Random azar = new Random();
                Medico[] alAzar = subordinados.ToArray();
                alAzar[azar.Next(alAzar.Count())].atenderEnfermo(unaPersona);

            }
            else
            {
                throw new EnfermoException();
            }
        }

    }

    /////////////////////////// EXCEPCIONES ///////////////////////////
    class EnfermoException : Exception { }

}
