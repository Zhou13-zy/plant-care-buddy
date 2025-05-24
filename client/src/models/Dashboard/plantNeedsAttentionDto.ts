export interface PlantNeedsAttentionDto {
    id: string;
    name: string;
    species: string;
    healthStatus: string;
    attentionReason: string;
    nextCareDate?: string;
    careType: string;
    strategyName: string;
}