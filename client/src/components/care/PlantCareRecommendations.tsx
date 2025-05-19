// client/src/components/plants/PlantCareRecommendations.tsx
import React, { useEffect, useState } from 'react';
import { getCareRecommendations } from '../../api/careRecommendationService';
import { CareRecommendation } from '../../models/Care/careRecommendation';
import './PlantCareRecommendations.css';

interface PlantCareRecommendationsProps {
  plantId: number;
}

const PlantCareRecommendations: React.FC<PlantCareRecommendationsProps> = ({ plantId }) => {
  const [recommendation, setRecommendation] = useState<CareRecommendation | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchRecommendations = async () => {
      try {
        setLoading(true);
        const data = await getCareRecommendations(plantId);
        setRecommendation(data);
        setError(null);
      } catch (error) {
        console.error('Error fetching care recommendations:', error);
        setError('Unable to load care recommendations');
      } finally {
        setLoading(false);
      }
    };

    fetchRecommendations();
  }, [plantId]);

  if (loading) return <div className="loading-recommendations">Loading care recommendations...</div>;
  if (error) return <div className="error-recommendations">{error}</div>;
  if (!recommendation) return <div className="no-recommendations">No care recommendations available</div>;

  return (
    <div className="care-recommendations-container">
      <div className="care-strategy-header">
        <h3>Care Strategy: {recommendation.strategyName}</h3>
        <p className="strategy-description">{recommendation.strategyDescription}</p>
        <div className="current-season">Current Season: {recommendation.currentSeason}</div>
      </div>

      <div className="care-schedule">
        <div className="care-item watering">
          <div className="care-icon">💧</div>
          <div className="care-details">
            <h4>Watering</h4>
            <p>Every {recommendation.wateringIntervalDays} days</p>
            <p className="next-date">
              Next: {new Date(recommendation.nextWateringDate).toLocaleDateString()}
            </p>
          </div>
        </div>

        {recommendation.fertilizingIntervalDays > 0 && (
          <div className="care-item fertilizing">
            <div className="care-icon">🌱</div>
            <div className="care-details">
              <h4>Fertilizing</h4>
              <p>Every {recommendation.fertilizingIntervalDays} days</p>
              {recommendation.nextFertilizingDate && (
                <p className="next-date">
                  Next: {new Date(recommendation.nextFertilizingDate).toLocaleDateString()}
                </p>
              )}
            </div>
          </div>
        )}
      </div>

      <div className="environment-recommendations">
        <div className="env-item light">
          <h4>Light</h4>
          <p>{recommendation.lightRecommendation}</p>
        </div>
        <div className="env-item humidity">
          <h4>Humidity</h4>
          <p>{recommendation.humidityRecommendation}</p>
        </div>
      </div>
    </div>
  );
};

export default PlantCareRecommendations;