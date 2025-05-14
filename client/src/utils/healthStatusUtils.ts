import { PlantHealthStatus } from '../models/Plant/plantHealthStatus';

/**
 * Gets the display name for a health status enum value
 */
export function getHealthStatusName(status: PlantHealthStatus): string {
  return PlantHealthStatus[status];
}

/**
 * Gets a CSS class-friendly lowercase string for a health status
 */
export function getHealthStatusClass(status: PlantHealthStatus): string {
  return PlantHealthStatus[status].toLowerCase();
}

/**
 * Returns options for a select dropdown of health statuses
 */
export function getHealthStatusOptions() {
  return Object.entries(PlantHealthStatus)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => ({
      value: value as number,
      label: key
    }));
}