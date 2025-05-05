from fastapi import FastAPI, HTTPException
import requests
import os

app = FastAPI(
    title="Orquestador - Historias Clínicas",
    description="Microservicio orquestador que combina datos de Pacientes y Consultas",
    version="1.0.0"
)

MS1_URL = os.getenv("MS1_URL", "http://pacientes:8000")
MS2_URL = os.getenv("MS2_URL", "http://consultas:3000")

@app.get("/historia/{mascota_id}", summary="Obtener la historia clínica completa de una mascota")
def obtener_historia_clinica(mascota_id: int):
    paciente_res = requests.get(f"{MS1_URL}/pacientes/{mascota_id}")
    if paciente_res.status_code != 200:
        raise HTTPException(status_code=paciente_res.status_code,
                            detail=f"MS1 error: No se pudo obtener datos del paciente {mascota_id}")
    paciente_data = paciente_res.json()

    consultas_res = requests.get(f"{MS2_URL}/consultas?mascota_id={mascota_id}")
    if consultas_res.status_code != 200:
        raise HTTPException(status_code=consultas_res.status_code,
                            detail=f"MS2 error: No se pudo obtener consultas de la mascota {mascota_id}")
    consultas_data = consultas_res.json()

    historia_completa = {
        "paciente": paciente_data,
        "consultas": consultas_data
    }
    return historia_completa