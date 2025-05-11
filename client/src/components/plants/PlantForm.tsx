import React, { useState } from 'react';
import { CreatePlantDto } from '../../models/createPlantDto';
import { UpdatePlantDto } from '../../models/updatePlantDto';
import { healthStatusOptions } from '../../utils/plantUtils';
import './PlantForm.css';

interface PlantFormProps<T extends CreatePlantDto | UpdatePlantDto> {
  onSubmit: (data: T) => void;
  initialData?: T;
  isEdit?: boolean;
}

const PlantForm = <T extends CreatePlantDto | UpdatePlantDto>({ 
  onSubmit, 
  initialData, 
  isEdit 
}: PlantFormProps<T>) => {
  const [form, setForm] = useState<T>(
    initialData || {
      name: '',
      species: '',
      acquisitionDate: '',
      location: '',
      healthStatus: 0,
      notes: '',
      primaryImagePath: '',
      ...(isEdit ? { id: 0 } : {})
    } as T
  );

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: name === 'healthStatus' ? Number(value) : value,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <form onSubmit={handleSubmit} className="plant-form">
      <div className="form-group">
        <label htmlFor="name">Name:</label>
        <input
          type="text"
          id="name"
          name="name"
          value={form.name}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="species">Species:</label>
        <input
          type="text"
          id="species"
          name="species"
          value={form.species}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="acquisitionDate">Acquisition Date:</label>
        <input
          type="date"
          id="acquisitionDate"
          name="acquisitionDate"
          value={form.acquisitionDate}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="location">Location:</label>
        <input
          type="text"
          id="location"
          name="location"
          value={form.location}
          onChange={handleChange}
        />
      </div>

      <div className="form-group">
        <label htmlFor="healthStatus">Health Status:</label>
        <select
          id="healthStatus"
          name="healthStatus"
          value={form.healthStatus}
          onChange={handleChange}
          required
        >
          {healthStatusOptions.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
      </div>

      <div className="form-group">
        <label htmlFor="notes">Notes:</label>
        <textarea
          id="notes"
          name="notes"
          value={form.notes}
          onChange={handleChange}
        />
      </div>

      <div className="form-group">
        <label htmlFor="primaryImagePath">Primary Image Path:</label>
        <input
          type="text"
          id="primaryImagePath"
          name="primaryImagePath"
          value={form.primaryImagePath}
          onChange={handleChange}
        />
      </div>

      <button type="submit" className="submit-button">
        {initialData ? 'Update Plant' : 'Add Plant'}
      </button>
    </form>
  );
};

export default PlantForm;