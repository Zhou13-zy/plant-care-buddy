import api from './axiosConfig';
import { Plant } from '../models/Plant/plant';
import { CreatePlantDto } from '../models/Plant/createPlantDto';
import { UpdatePlantDto } from '../models/Plant/updatePlantDto';

export const getAllPlants = async (): Promise<Plant[]> => {
  const response = await api.get<Plant[]>('/plants');
  return response.data;
};

export const getPlantById = async (id: number | string): Promise<Plant> => {
    const response = await api.get<Plant>(`/plants/${id}`);
    return response.data;
};

export const addPlant = async (plant: CreatePlantDto) => {
  const formData = new FormData();
  formData.append('name', plant.name);
  formData.append('species', plant.species);
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

export const updatePlant = async (id: number, data: UpdatePlantDto): Promise<Plant> => {
  const response = await api.put<Plant>(`/plants/${id}`, data);
  return response.data;
};

export const deletePlant = async (id: number | string): Promise<void> => {
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

// add more methods here later (updatePlant, deletePlant, etc.)