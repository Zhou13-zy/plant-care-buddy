import { DashboardStatsDto } from "./dashboardStatsDto";
import { PlantNeedsAttentionDto } from "./plantNeedsAttentionDto";
import { UpcomingCareDto } from "./upcomingCareDto";

export interface DashboardOverviewDto {
    plantsNeedingAttention: PlantNeedsAttentionDto[];
    upcomingCareTasks: UpcomingCareDto[];
    stats: DashboardStatsDto;
}