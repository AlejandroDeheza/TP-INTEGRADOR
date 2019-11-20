//////////////////////////////////////PERSONA///////////////////////////////////////

class Persona {

	var property temperatura = 36 // valor arbitrario (temperatura normal media del cuerpo)
	var property cantidadCelulas = 30000000 //30 millones. valor arbitrario
	var enfermedades = #{}
	var property cuantosDiasVivio = 0

	method contraerEnfermedad(enfermedad) {
		enfermedades.add(enfermedad)
	}
	
	method vivirUnDia(){
		cuantosDiasVivio += 1
		if(!enfermedades.isEmpy()){
			enfermedades.forEach{enfermedad => enfermedad.afectarPersona(self)}
			}
	}
	
	method estaEnComa() = temperatura >= 45
	
}

///////////////////////////////////////ENFERMEDADES////////////////////////////////////

class Enfermedad{
	
	var cantCelulasQueAmenaza
	
	method afectarPersona(unaPersona)
	
	method esAgresivaPara(unaPersona)
}

class EnfermedadInfecciosa inherits Enfermedad{
	
	override method afectarPersona(unaPersona){
		if(unaPersona.temperatura() < 45){
			self.aumentarTemperatura(unaPersona)
		}
	}
	
	method aumentarTemperatura(unaPersona){
		unaPersona.temperatura(unaPersona.temperatura() + cantCelulasQueAmenaza / 1000)
	}
	
	method reproducirse(){
		self.duplicarCelulasAmenazadas()
	}
	
	method duplicarCelulasAmenazadas(){
		cantCelulasQueAmenaza *= 2
	}
	
	override method esAgresivaPara(unaPersona){
		return cantCelulasQueAmenaza > unaPersona.cantidadCelulas()/10
	}
}

class EnfermedadAutoinmune inherits Enfermedad{
	
	override method afectarPersona(unaPersona){
		self.destruirCelulas(unaPersona)
		
	}
	
	method destruirCelulas(unaPersona){
		unaPersona.cantidadCelulas(unaPersona.cantidadCelulas() - cantCelulasQueAmenaza)
	}
	
	override method esAgresivaPara(unaPersona){
		return unaPersona.cuantosDiasVivio() > 30
	}
}

///////////////////////////////////////PARA TESTS//////////////////////////////////

const malaria500 = new EnfermedadInfecciosa(
	
	cantCelulasQueAmenaza = 500
)

const malaria800 = new EnfermedadInfecciosa(
	
	cantCelulasQueAmenaza = 800
)

const otitis = new EnfermedadInfecciosa(
	
	cantCelulasQueAmenaza = 100
)

const lupus = new EnfermedadAutoinmune(
	
	cantCelulasQueAmenaza = 10000
)

const logan = new Persona(
	
	cantidadCelulas = 3000000,
	enfermedades = #{malaria500, otitis, lupus}
)

const frank = new Persona(
	
	cantidadCelulas = 3500000
)

/////////////////////////////////////////temporada 2//////////////////////////////////////

/* 

class Medico inherits Persona{ // TEMPORADA 2 - USANDO SUPER()

	override method contraerEnfermedad(enfermedad) {
		super(enfermedad)
		self.tomar(100) // valor arbitrario
	}//IMPLEMENTAR tomar(n)
}

*/