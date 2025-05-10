import api from './axiosConfig';
import { Plant } from '../models/plant';

export const getAllPlants = async (): Promise<Plant[]> => {
  const response = await api.get<Plant[]>('/plants');
  return response.data;
};

// add more methods here later (addPlant, updatePlant, deletePlant, etc.)