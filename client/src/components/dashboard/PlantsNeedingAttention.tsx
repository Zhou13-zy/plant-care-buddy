import React from "react";
import { PlantNeedsAttentionDto } from "../../models/Dashboard/plantNeedsAttentionDto";
import styles from "./dashboard.module.css";
import { useNavigate } from "react-router-dom";

interface Props {
  plants: PlantNeedsAttentionDto[];
}

const PlantsNeedingAttention: React.FC<Props> = ({ plants }) => {
  const navigate = useNavigate();

  console.log(plants);

  return (
    <section className={styles.section}>
      <h3>Plants Needing Attention</h3>
      {plants.length === 0 ? (
        <p>All plants are healthy!</p>
      ) : (
        <div>
          {plants.map(plant => {
            const isUrgent = plant.attentionReason.toLowerCase().includes("overdue");
            return (
              <div
                key={plant.id}
                className={`${styles.attentionCard} ${isUrgent ? styles.urgent : ""}`}
              >
                <div className={styles.attentionCardHeader}>
                  <span className={styles.attentionCardTitle}>
                    {plant.name} <span style={{ fontWeight: 400, color: "#555" }}>({plant.species})</span>
                  </span>
                </div>
                <div className={styles.attentionCardDetails}>
                  <b>Reason:</b> {plant.attentionReason}
                  {plant.nextCareDate && (
                    <> | <b>Next Care:</b> {new Date(plant.nextCareDate).toLocaleDateString()} ({plant.careType})</>
                  )}
                  <br />
                  <b>Strategy:</b> {plant.strategyName}
                </div>
                <div className={styles.attentionCardFooter}>
                  <button
                    className={styles.actionButton}
                    onClick={() => navigate(`/plants/${plant.id}`)}
                  >
                    View Details
                  </button>
                </div>
              </div>
            );
          })}
        </div>
      )}
    </section>
  );
};

export default PlantsNeedingAttention;
