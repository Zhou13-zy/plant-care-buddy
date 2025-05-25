import api from './axiosConfig';
import { Reminder } from '../models/Reminder/reminder';
import { CreateReminderDto } from '../models/Reminder/createReminderDto';
import { UpdateReminderDto } from '../models/Reminder/updateReminderDto';

// Get all reminders for a plant
export const getRemindersByPlantId = async (plantId: string): Promise<Reminder[]> => {
  const response = await api.get<Reminder[]>(`/reminders/plant/${plantId}`);
  return response.data;
};

// Get a single reminder by ID
export const getReminderById = async (id: string): Promise<Reminder> => {
  const response = await api.get<Reminder>(`/reminders/${id}`);
  return response.data;
};

// Create a new reminder
export const createReminder = async (data: CreateReminderDto): Promise<Reminder> => {
  const response = await api.post<Reminder>('/reminders', data);
  return response.data;
};

// Update an existing reminder
export const updateReminder = async (id: string, data: UpdateReminderDto): Promise<Reminder> => {
  const response = await api.put<Reminder>(`/reminders/${id}`, data);
  return response.data;
};

// Delete a reminder
export const deleteReminder = async (id: string): Promise<void> => {
  await api.delete(`/reminders/${id}`);
};

// Mark a reminder as complete
export const markReminderAsComplete = async (id: string): Promise<Reminder> => {
  const response = await api.post<Reminder>(`/reminders/${id}/complete`);
  return response.data;
};

export const getAllReminders = async (): Promise<Reminder[]> => {
  const response = await api.get<Reminder[]>('/reminders');
  return response.data;
};

export const generateRemindersForPlant = async (plantId: string): Promise<Reminder[]> => {
  const response = await api.post<Reminder[]>(`/reminders/${plantId}/generate-strategy-reminders`);
  return response.data;
};
