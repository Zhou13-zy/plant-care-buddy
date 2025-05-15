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
  const response = await api.post<HealthObservation>('/health-observations', healthObservation);
  return response.data;
};

export const updateHealthObservation = async (id: number, healthObservation: UpdateHealthObservationDto): Promise<HealthObservation> => {
  const response = await api.put<HealthObservation>(`/health-observations/${id}`, healthObservation);
  return response.data;
};

export const deletePlant = async (id: number | string): Promise<void> => {
    await api.delete(`/health-observations/${id}`);
  };