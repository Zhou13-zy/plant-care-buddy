import api from './axiosConfig';
import { ReminderDto } from '../models/Reminder/reminderDto';
import { CreateReminderDto } from '../models/Reminder/createReminderDto';
import { UpdateReminderDto } from '../models/Reminder/updateReminderDto';

// Get all reminders for a plant
export const getRemindersByPlantId = async (plantId: string): Promise<ReminderDto[]> => {
  const response = await api.get<ReminderDto[]>(`/reminders/plant/${plantId}`);
  return response.data;
};

// Get a single reminder by ID
export const getReminderById = async (id: string): Promise<ReminderDto> => {
  const response = await api.get<ReminderDto>(`/reminders/${id}`);
  return response.data;
};

// Create a new reminder
export const createReminder = async (data: CreateReminderDto): Promise<ReminderDto> => {
  const response = await api.post<ReminderDto>('/reminders', data);
  return response.data;
};

// Update an existing reminder
export const updateReminder = async (id: string, data: UpdateReminderDto): Promise<ReminderDto> => {
  const response = await api.put<ReminderDto>(`/reminders/${id}`, data);
  return response.data;
};

// Delete a reminder
export const deleteReminder = async (id: string): Promise<void> => {
  await api.delete(`/reminders/${id}`);
};

// Mark a reminder as complete
export const markReminderAsComplete = async (id: string): Promise<ReminderDto> => {
  const response = await api.post<ReminderDto>(`/reminders/${id}/complete`);
  return response.data;
};

export const getAllReminders = async (): Promise<ReminderDto[]> => {
  const response = await api.get<ReminderDto[]>('/reminders');
  return response.data;
};