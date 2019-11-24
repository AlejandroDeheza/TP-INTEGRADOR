using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_INTEGRADOR 
{
    class Program 
    {
        static void Main(string[] args) 
        {

        }
    }

    class Persona 
    {

        private int temperatura = 36; // temperatura normal
        private int cantidadCelulas = 30000000; // 30 millones. valor arbitrario
        private int cuantosDiasVivio = 0;
        private HashSet<Enfermedad> enfermedades = new HashSet<Enfermedad>(); 

        public int TEMPERATURA
        {
            get => temperatura;
            set => temperatura = value;
        }

        public int CANTIDADCELULAS
        {
            get => cantidadCelulas;
            set => cantidadCelulas = value;
        }

        public int CUANTOSDIASVIVIO
        {
            get => cuantosDiasVivio;
        }

        public HashSet<Enfermedad> ENFERMEDADES
        {
            get => enfermedades;
        }

        public virtual void contraerEnfermedad(Enfermedad enfermedad) 
        {
            enfermedades.Add(enfermedad);
        }

        public bool estaSano() => enfermedades.Count() == 0;

        public void vivirUnDia()
        {
            cuantosDiasVivio += 1;

            if (!this.estaSano())
            {
                foreach (Enfermedad enfermedad in enfermedades)
                {
                    enfermedad.afectarPersona(this);
                }
            }
        }

        public bool estaEnComa() => temperatura >= 45 || cantidadCelulas < 1000000;


        public void recibirDosis(int unaDosis) 
        {
            if (!this.estaMuerto()) 
            {
                foreach (Enfermedad enfermedad in enfermedades)
                {
                    enfermedad.atenuar(unaDosis * 15);
                }
                this.curarseDeEnfermedadesInhabilitadas();
            }
        }

        private void curarseDeEnfermedadesInhabilitadas()  
            // metodo auxiliar, no tiene sentido que sea publico
        {
            foreach (Enfermedad enfermedad in enfermedades)
            {
                if (enfermedad.CANTCELULASQUEAMENAZA <= 0) 
                    enfermedades.Remove(enfermedad);
            }
        }

        public bool estaMuerto() => enfermedades.Contains(muerte);

    }

    ///////////////////////////////////////ENFERMEDADES////////////////////////////////////
    abstract class Enfermedad
    {

        private int cantCelulasQueAmenaza;

        public int CANTCELULASQUEAMENAZA
        {
            get => cantCelulasQueAmenaza;
            set => cantCelulasQueAmenaza = value;
        }

        public abstract void afectarPersona(Persona unaPersona);

        public abstract bool esAgresivaPara(Persona unaPersona);

        public virtual void atenuar(int atenuacion)
        {
            cantCelulasQueAmenaza -= atenuacion;
        }

    }

    class EnfermedadInfecciosa : Enfermedad
    {

        public override void afectarPersona(Persona unaPersona)
        {
            if (unaPersona.TEMPERATURA < 45)
                this.aumentarTemperatura(unaPersona);
        }

        public void aumentarTemperatura(Persona unaPersona)
        {
            unaPersona.TEMPERATURA += CANTCELULASQUEAMENAZA / 1000;
        }

        public void reproducirse()
        {
            CANTCELULASQUEAMENAZA *= 2;
        }

        public override bool esAgresivaPara(Persona unaPersona)
        {
            return CANTCELULASQUEAMENAZA > unaPersona.CANTIDADCELULAS / 10;
        }

    }

    class EnfermedadAutoinmune : Enfermedad
    {
        public override void afectarPersona(Persona unaPersona)
        {
            this.destruirCelulas(unaPersona);
        }

        public void destruirCelulas(Persona unaPersona)
        {
            unaPersona.CANTIDADCELULAS -= CANTCELULASQUEAMENAZA;
        }

        public override bool esAgresivaPara(Persona unaPersona)
        {
            return unaPersona.CUANTOSDIASVIVIO > 30;
        }

    }

    //////////////////////////////////////temporada 2////////////////////////////////////
    class EnfermoException : Exception { }

    class Medico : Persona
    {

        private int dosis = 100; // valor arbitrario

        public int DOSIS
        {
            get => dosis;
            set => dosis = value;
        }

        public virtual void atenderEnfermo(Persona unaPersona)
        {
            if (!unaPersona.estaSano())
                this.darDosis(unaPersona);
            else
                throw new EnfermoException(
                    message = "La persona no esta enferma, " +
                    "el doctor no le tiene que curar nada"
                );
        }

        public void darDosis(Persona unaPersona)
        {
            unaPersona.recibirDosis(dosis);
        }

        public override void contraerEnfermedad(Enfermedad enfermedad)
        {
            base.contraerEnfermedad(enfermedad); // esto funciona como deberia?
            //"base" es lo mismo que super()?
            this.darDosis(this);
        }

    }

    class JefeDeDepartamento : Medico
    {
        private HashSet<Medico> subordinados = new HashSet<Medico>();

        public HashSet<Medico> SUBORDINADOS
        {
            get => subordinados;
        }

        public override void atenderEnfermo(Persona unaPersona)
        {
            if (!unaPersona.estaSano())
            {
                subordinados.anyOne().darDosis(unaPersona); 
                //hay que buscar la forma de que devuelva un subordinado aleatorio..
            }
            else
            {
                throw new EnfermoException(
                    message = "La persona no esta enferma, el doctor no le tiene que curar nada"
                );
            }
        }

    }

    object muerte : Enfermedad      //NO HAY WKO EN C#
    {   // en el constructor hay que poner (cantCelulasQueAmenaza = 0)

	    public override void afectarPersona(Persona unaPersona)
        {
            this.disminuirTemperatura(unaPersona);
        }

        public void disminuirTemperatura(Persona unaPersona)
        {
            unaPersona.TEMPERATURA = 0;
        }

        public override bool esAgresivaPara(Persona unaPersona) => true;

        public override void atenuar(int atenuacion)
        {
            // no hace nada
        }
    }
    
}
