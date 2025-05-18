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
  const formData = new FormData();
  formData.append('plantId', careEvent.plantId.toString());
  formData.append('eventType', careEvent.eventType.toString());
  formData.append('eventDate', careEvent.eventDate);
  if (careEvent.notes) formData.append('notes', careEvent.notes);
  if (careEvent.beforePhoto) formData.append('beforePhoto', careEvent.beforePhoto);
  if (careEvent.afterPhoto) formData.append('afterPhoto', careEvent.afterPhoto);

  const response = await api.post<CareEvent>('/care-events', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
};

export const updateCareEvent = async (id: number, careEvent: UpdateCareEventDto): Promise<CareEvent> => {
  const formData = new FormData();
  formData.append('eventType', careEvent.eventType.toString());
  formData.append('eventDate', careEvent.eventDate);
  if (careEvent.notes) formData.append('notes', careEvent.notes);
  if (careEvent.beforePhoto) formData.append('beforePhoto', careEvent.beforePhoto);
  if (careEvent.afterPhoto) formData.append('afterPhoto', careEvent.afterPhoto);

  const response = await api.put<CareEvent>(`/care-events/${id}`, formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
};

export const deleteCareEvent = async (id: number): Promise<void> => {
  await api.delete(`/care-events/${id}`);
};