import api from './axiosConfig';
import { HealthObservation } from '../models/HealthObservation/healthObservation';
import { CreateHealthObservationDto } from '../models/HealthObservation/createHealthObservationDto';
import { UpdateHealthObservationDto } from '../models/HealthObservation/updateHealthObservationDto';


export const getHealthObservations = async (): Promise<HealthObservation[]> => {
  const response = await api.get<HealthObservation[]>('/health-observations');
  return response.data;
};

export const getHealthObservationsByPlantId = async (plantId: number): Promise<HealthObservation[]> => {
  const response = await api.get<HealthObservation[]>(`/health-observations/plant/${plantId}`);
  return response.data;
};

export const getHealthObservation = async (id: number): Promise<HealthObservation> => {
  const response = await api.get<HealthObservation>(`/health-observations/${id}`);
  return response.data;
};

export const createHealthObservation = async (healthObservation: CreateHealthObservationDto): Promise<HealthObservation> => {
  const formData = new FormData();
  formData.append('plantId', healthObservation.plantId.toString());
  formData.append('observationDate', healthObservation.observationDate);
  formData.append('healthStatus', healthObservation.healthStatus.toString());
  formData.append('notes', healthObservation.notes);
  if (healthObservation.photo) {
    formData.append('photo', healthObservation.photo);
  }

  const response = await api.post<HealthObservation>('/health-observations', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
};

export const updateHealthObservation = async (id: number, healthObservation: UpdateHealthObservationDto): Promise<HealthObservation> => {
  const formData = new FormData();
  formData.append('observationDate', healthObservation.observationDate);
  formData.append('healthStatus', healthObservation.healthStatus.toString());
  formData.append('notes', healthObservation.notes);
  if (healthObservation.photo) {
    formData.append('photo', healthObservation.photo);
  }

  const response = await api.put<HealthObservation>(`/health-observations/${id}`, formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
};

export const deletePlant = async (id: number | string): Promise<void> => {
    await api.delete(`/health-observations/${id}`);
  };