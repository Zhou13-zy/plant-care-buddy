import React, { useState } from 'react';
import { healthStatusOptions } from '../../utils/plantUtils';
import './SearchBar.css';

interface SearchBarProps {
  filters: {
    name: string;
    species: string;
    healthStatus: string;
    location: string;
  };
  setFilters: React.Dispatch<React.SetStateAction<{
    name: string;
    species: string;
    healthStatus: string;
    location: string;
  }>>;
  onSearch: (filters: {
    name?: string;
    species?: string;
    healthStatus?: number;
    location?: string;
  }) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ filters, setFilters, onSearch }) => {
  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFilters((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch({
      ...filters,
      healthStatus: filters.healthStatus ? Number(filters.healthStatus) : undefined
    });
  };

  return (
    <form className="search-bar" onSubmit={handleSubmit}>
      <input
        type="text"
        name="name"
        placeholder="Name"
        value={filters.name}
        onChange={handleChange}
      />
      <input
        type="text"
        name="species"
        placeholder="Species"
        value={filters.species}
        onChange={handleChange}
      />
      <select
        name="healthStatus"
        value={filters.healthStatus}
        onChange={handleChange}
      >
        <option value="">All Health Statuses</option>
        {healthStatusOptions.map((option) => (
          <option key={option.value} value={option.value}>{option.label}</option>
        ))}
      </select>
      <input
        type="text"
        name="location"
        placeholder="Location"
        value={filters.location}
        onChange={handleChange}
      />
      <button type="submit">Search</button>
      <button
        type="button"
        onClick={() => {
          setFilters({ name: '', species: '', healthStatus: '', location: '' });
          onSearch({});
        }}
      >
        Reset
      </button>
    </form>
  );
};

export default SearchBar;