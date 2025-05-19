import { PlantType } from '../models/Plant/plantType';

/**
 * Gets the display name for a plant type enum value
 */
export function getPlantTypeName(type: PlantType): string {
  return PlantType[type];
}

/**
 * Gets a CSS class-friendly lowercase string for a plant type
 */
export function getPlantTypeClass(type: PlantType): string {
  return PlantType[type].toLowerCase();
}

/**
 * Returns options for a select dropdown of plant types
 */
export function getPlantTypeOptions() {
  return Object.entries(PlantType)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => ({
      value: value as number,
      label: key
    }));
}
