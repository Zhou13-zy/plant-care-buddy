import api from './axiosConfig';
import { CareEvent } from '../models/CareEvent/careEvent';
import { CreateCareEventDto } from '../models/CareEvent/createCareEventDto';
import { UpdateCareEventDto } from '../models/CareEvent/updateCareEventDto';

export const getAllCareEvents = async (): Promise<CareEvent[]> => {
  const response = await api.get<CareEvent[]>('/care-events');
  return response.data;
};

export const getCareEventsByPlant = async (plantId: number): Promise<CareEvent[]> => {
  const response = await api.get<CareEvent[]>(`/care-events/plant/${plantId}`);
  return response.data;
};

export const getCareEventById = async (id: number): Promise<CareEvent> => {
  const response = await api.get<CareEvent>(`/care-events/${id}`);
  return response.data;
};

export const createCareEvent = async (careEvent: CreateCareEventDto): Promise<CareEvent> => {
  const response = await api.post<CareEvent>('/care-events', careEvent);
  return response.data;
};

export const updateCareEvent = async (id: number, careEvent: UpdateCareEventDto): Promise<CareEvent> => {
  const response = await api.put<CareEvent>(`/care-events/${id}`, careEvent);
  return response.data;
};

export const deleteCareEvent = async (id: number): Promise<void> => {
  await api.delete(`/care-events/${id}`);
};