import api from './axiosConfig';
import { CareRecommendation } from '../models/Care/careRecommendation';

/**
 * Get care recommendations for a plant
 */
export const getCareRecommendations = async (plantId: string): Promise<CareRecommendation> => {
    const response = await api.get(`/plants/${plantId}/care-recommendations`);
    return response.data;
};