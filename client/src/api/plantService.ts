import api from './axiosConfig';
import { Plant } from '../models/Plant/plant';
import { CreatePlantDto } from '../models/Plant/createPlantDto';
import { UpdatePlantDto } from '../models/Plant/updatePlantDto';

export const getAllPlants = async (): Promise<Plant[]> => {
  const response = await api.get<Plant[]>('/plants');
  return response.data;
};

export const getPlantById = async (id: string): Promise<Plant> => {
    const response = await api.get<Plant>(`/plants/${id}`);
    return response.data;
};

export const addPlant = async (plant: CreatePlantDto) => {
  const formData = new FormData();
  formData.append('name', plant.name);
  formData.append('species', plant.species);
  formData.append('plantType', plant.plantType.toString());
  formData.append('acquisitionDate', plant.acquisitionDate);
  formData.append('location', plant.location);
  formData.append('healthStatus', plant.healthStatus.toString());
  if (plant.nextHealthCheckDate) formData.append('nextHealthCheckDate', plant.nextHealthCheckDate);
  if (plant.notes) formData.append('notes', plant.notes);
  if (plant.photo) formData.append('photo', plant.photo);

  const response = await api.post('/plants', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
};

export const updatePlant = async (id: string, data: UpdatePlantDto): Promise<Plant> => {
  const formData = new FormData();
  formData.append('name', data.name);
  formData.append('species', data.species);
  formData.append('plantType', data.plantType.toString());
  formData.append('acquisitionDate', data.acquisitionDate);
  formData.append('location', data.location);
  if (data.nextHealthCheckDate) formData.append('nextHealthCheckDate', data.nextHealthCheckDate);
  if (data.notes) formData.append('notes', data.notes);
  if (data.photo) formData.append('photo', data.photo);

  const response = await api.put<Plant>(`/plants/${id}`, formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return response.data;
};

export const deletePlant = async (id: string): Promise<void> => {
  await api.delete(`/plants/${id}`);
};

export const searchPlants = async (filters: {
  name?: string;
  species?: string;
  healthStatus?: number;
  location?: string;
}): Promise<Plant[]> => {
  const params = new URLSearchParams();
  if (filters.name) params.append('name', filters.name);
  if (filters.species) params.append('species', filters.species);
  if (filters.healthStatus !== undefined) params.append('healthStatus', String(filters.healthStatus));
  if (filters.location) params.append('location', filters.location);

  const response = await api.get<Plant[]>(`/plants/search?${params.toString()}`);
  return response.data;
};