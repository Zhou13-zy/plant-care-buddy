import React from 'react';
import PlantForm from '../components/plants/PlantForm';
import { addPlant } from '../api/plantService';
import { useNavigate } from 'react-router-dom';
import { CreatePlantDto } from '../models/Plant/createPlantDto';
import './AddPlantPage.css';

const AddPlantPage: React.FC = () => {
  const navigate = useNavigate();

  const handleAddPlant = async (data: CreatePlantDto) => {
    try {
      await addPlant(data);
      alert('Plant added successfully!');
      navigate('/plants'); // Redirect to plant list or wherever you want
    } catch (error) {
      alert('Failed to add plant.');
    }
  };

  return (
    <div className="add-plant-page-container">
      <PlantForm<CreatePlantDto> onSubmit={handleAddPlant} />
    </div>
  );
};

export default AddPlantPage;