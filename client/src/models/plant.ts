export interface Plant {
    id: number;
    name: string;
    species: string;
    acquisitionDate: string;
    location: string;
    healthStatus: string;
    notes?: string;
    primaryImagePath?: string;
  }