import React from "react";
import { PlantNeedsAttentionDto } from "../../models/Dashboard/plantNeedsAttentionDto";
import styles from "./dashboard.module.css";

interface Props {
  plants: PlantNeedsAttentionDto[];
}

const PlantsNeedingAttention: React.FC<Props> = ({ plants }) => (
  <section className={styles.section}>
    <h3>Plants Needing Attention</h3>
    {plants.length === 0 ? (
      <p>All plants are healthy!</p>
    ) : (
      <ul className={styles.attentionList}>
        {plants.map(plant => (
          <li key={plant.id}>
            <strong>{plant.name}</strong> ({plant.species}) — {plant.attentionReason}
            {plant.nextCareDate && (
              <> | Next Care: {new Date(plant.nextCareDate).toLocaleDateString()} ({plant.careType})</>
            )}
            {plant.strategyName && <> | Strategy: {plant.strategyName}</>}
          </li>
        ))}
      </ul>
    )}
  </section>
);

export default PlantsNeedingAttention;
