//////////////////////////////////////PERSONA///////////////////////////////////////
class Persona {

	var property temperatura = 36 // temperatura normal
	var property cantidadCelulas = 30000000 // 30 millones. valor arbitrario
	const property enfermedades = #{}
	var property cuantosDiasVivio = 0

	method contraerEnfermedad(enfermedad) {
		enfermedades.add(enfermedad)
	}

	method vivirUnDia() {
		cuantosDiasVivio += 1
		if (!enfermedades.isEmpty()) {
			enfermedades.forEach{ enfermedad => enfermedad.afectarPersona(self)}
		}
	}

	method estaEnComa() = temperatura >= 45 || cantidadCelulas < 1000000

}

///////////////////////////////////////ENFERMEDADES////////////////////////////////////
class Enfermedad {

	var property cantCelulasQueAmenaza // el getter se usa en los tests

	method afectarPersona(unaPersona)

	method esAgresivaPara(unaPersona)

}

class EnfermedadInfecciosa inherits Enfermedad {

	override method afectarPersona(unaPersona) {
		if (unaPersona.temperatura() < 45) {
			self.aumentarTemperatura(unaPersona)
		}
	}

	method aumentarTemperatura(unaPersona) {
		unaPersona.temperatura(unaPersona.temperatura() + cantCelulasQueAmenaza / 1000)
	}

	method reproducirse() {
		cantCelulasQueAmenaza *= 2
	}

	override method esAgresivaPara(unaPersona) {
		return cantCelulasQueAmenaza > unaPersona.cantidadCelulas() / 10
	}

}

class EnfermedadAutoinmune inherits Enfermedad {

	override method afectarPersona(unaPersona) {
		self.destruirCelulas(unaPersona)
	}

	method destruirCelulas(unaPersona) {
		unaPersona.cantidadCelulas(unaPersona.cantidadCelulas() - cantCelulasQueAmenaza)
	}

	override method esAgresivaPara(unaPersona) {
		return unaPersona.cuantosDiasVivio() > 30
	}

}

/////////////////////////////////////////temporada 2//////////////////////////////////////
/* 

 * class Medico inherits Persona{ // TEMPORADA 2 - USANDO SUPER()

 * 	override method contraerEnfermedad(enfermedad) {
 * 		super(enfermedad)
 * 		self.tomar(100) // valor arbitrario
 * 	}//IMPLEMENTAR tomar(n)
 * }

 */
