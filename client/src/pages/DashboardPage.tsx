import React, { useEffect, useState } from "react";
import { getDashboardOverview } from "../api/dashboardService";
import { DashboardOverviewDto } from "../models/Dashboard/dashboardOverviewDto";
import DashboardStats from "../components/dashboard/DashboardStats";
import PlantsNeedingAttention from "../components/dashboard/PlantsNeedingAttention";
import UpcomingCareTasks from "../components/dashboard/UpcomingCareTasks";
import styles from "../components/dashboard/dashboard.module.css";

const DashboardPage: React.FC = () => {
  const [data, setData] = useState<DashboardOverviewDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    setLoading(true);
    getDashboardOverview()
      .then(setData)
      .catch(() => setError("Failed to load dashboard data"))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <div>Loading dashboard...</div>;
  if (error) return <div style={{ color: "red" }}>{error}</div>;
  if (!data) return <div>No dashboard data available.</div>;

  return (
    <div className={styles.dashboardContainer}>
      <h2>Dashboard</h2>
      <DashboardStats stats={data.stats} />
      <PlantsNeedingAttention plants={data.plantsNeedingAttention} />
      <UpcomingCareTasks tasks={data.upcomingCareTasks} />
    </div>
  );
};

export default DashboardPage;
