import api from './axiosConfig';
import { Plant } from '../models/plant';
import { CreatePlantDto } from '../models/createPlantDto';
import { UpdatePlantDto } from '../models/updatePlantDto';

export const getAllPlants = async (): Promise<Plant[]> => {
  const response = await api.get<Plant[]>('/plants');
  return response.data;
};

export const getPlantById = async (id: number | string): Promise<Plant> => {
    const response = await api.get<Plant>(`/plants/${id}`);
    return response.data;
};

export const addPlant = async (plant: CreatePlantDto) => {
    const response = await api.post('/plants', plant);
    return response.data;
};

export const updatePlant = async (id: number, data: UpdatePlantDto): Promise<Plant> => {
  const response = await api.put<Plant>(`/plants/${id}`, data);
  return response.data;
};

// add more methods here later (updatePlant, deletePlant, etc.)