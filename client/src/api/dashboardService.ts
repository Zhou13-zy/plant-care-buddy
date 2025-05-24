import api from './axiosConfig';
import { DashboardOverviewDto } from '../models/Dashboard/dashboardOverviewDto';

export const getDashboardOverview = async (): Promise<DashboardOverviewDto> => {
    const response = await api.get<DashboardOverviewDto>('/Dashboard/overview');
    return response.data;
};