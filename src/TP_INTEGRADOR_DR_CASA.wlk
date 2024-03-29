//////////////////////////////////////PERSONA///////////////////////////////////////
class Persona {

	var property temperatura = 36 // temperatura normal
	var property cantidadCelulas = 30000000 // 30 millones. valor arbitrario
	const property enfermedades = #{}
	var property cuantosDiasVivio = 0

	method contraerEnfermedad(enfermedad) {
		enfermedades.add(enfermedad)
	}

	method estaSano() = enfermedades.isEmpty()

	method vivirUnDia() {
		cuantosDiasVivio += 1
		if (!self.estaSano()) {
			enfermedades.forEach{ enfermedad => enfermedad.afectarPersona(self)}
		}
	}

	method estaEnComa() = temperatura >= 45 || cantidadCelulas < 1000000

	method recibirDosis(unaDosis) {
		if (self.estaMuerto().negate()) {
			enfermedades.forEach{ enfermedad => enfermedad.atenuar(unaDosis * 15)}
			self.curarseDeEnfermedadesInhabilitadas()
		}
	}

	method curarseDeEnfermedadesInhabilitadas() {
		enfermedades.forEach{ enfermedad =>
			if (enfermedad.cantCelulasQueAmenaza() <= 0) {
				enfermedades.remove(enfermedad)
			}
		}
	}

	method estaMuerto() = enfermedades.contains(muerte)

}

///////////////////////////////////////ENFERMEDADES////////////////////////////////////
class Enfermedad {

	var property cantCelulasQueAmenaza

	method afectarPersona(unaPersona)

	method esAgresivaPara(unaPersona)

	method atenuar(atenuacion) {
		cantCelulasQueAmenaza -= atenuacion
	}

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

	override method esAgresivaPara(unaPersona) = unaPersona.cuantosDiasVivio() > 30

}

/////////////////////////////////////////temporada 2//////////////////////////////////////
class EnfermoException inherits Exception {

}

class Medico inherits Persona {

	var dosis = 100 // valor arbitrario

	method atenderEnfermo(unaPersona) {
		if (unaPersona.estaSano().negate()) {
			self.darDosis(unaPersona)
		} else {
			throw new EnfermoException (
				message = "La persona no esta enferma, el doctor no le tiene que curar nada"
			)
		}
	}

	method darDosis(unaPersona) {
		unaPersona.recibirDosis(dosis)
	}

	override method contraerEnfermedad(enfermedad) {
		super(enfermedad)
		self.darDosis(self)
	}

}

class JefeDeDepartamento inherits Medico {

	const subordinados = #{}

	override method atenderEnfermo(unaPersona) {
		if (unaPersona.estaSano().negate()) {
			subordinados.anyOne().darDosis(unaPersona)
		} else {
			throw new EnfermoException (
				message = "La persona no esta enferma, el doctor no le tiene que curar nada"
			)
		}
	}

}

object muerte inherits Enfermedad(cantCelulasQueAmenaza = 0) {

	override method afectarPersona(unaPersona) {
		self.disminuirTemperatura(unaPersona)
	}

	method disminuirTemperatura(unaPersona) {
		unaPersona.temperatura(0)
	}

	override method esAgresivaPara(unaPersona) = true

	override method atenuar(atenuacion) {
	// no hace nada
	}

}

