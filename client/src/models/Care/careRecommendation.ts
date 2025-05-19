export interface CareRecommendation {
    plantId: number;
    plantName: string;
    strategyName: string;
    strategyDescription: string;
    wateringIntervalDays: number;
    nextWateringDate: string;
    fertilizingIntervalDays: number;
    nextFertilizingDate: string | null;
    lightRecommendation: string;
    humidityRecommendation: string;
    currentSeason: string;
  }