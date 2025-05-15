import React from 'react';
import { PlantHealthStatus } from '../../models/Plant/plantHealthStatus';
import { getHealthStatusClass, getHealthStatusName } from '../../utils/healthStatusUtils';
import './HealthStatusIndicator.css';

interface HealthStatusIndicatorProps {
  status: PlantHealthStatus;
  showLabel?: boolean; // Optional: whether to show the text label
  size?: 'small' | 'medium' | 'large'; // Optional: size of the indicator
}

const HealthStatusIndicator: React.FC<HealthStatusIndicatorProps> = ({ 
  status, 
  showLabel = true,
  size = 'medium' 
}) => {
  const statusClass = getHealthStatusClass(status);
  
  return (
    <div className={`health-status-indicator ${statusClass} ${size}`}>
      <div className="indicator-icon"></div>
      {showLabel && (
        <span className="indicator-label">{getHealthStatusName(status)}</span>
      )}
    </div>
  );
};

export default HealthStatusIndicator;