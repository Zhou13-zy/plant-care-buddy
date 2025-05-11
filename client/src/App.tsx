import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import HomePage from './pages/HomePage';
import PlantsPage from './pages/PlantsPage';
import AddPlantPage from './pages/AddPlantPage';
import PlantDetailPage from './pages/PlantDetailPage';
import EditPlantPage from './pages/EditPlantPage';
function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/plants" element={<PlantsPage />} />
        <Route path="/add-plant" element={<AddPlantPage />} />
        <Route path="/plants/:id" element={<PlantDetailPage />} />
        <Route path="/plants/:id/edit" element={<EditPlantPage />} />
      </Routes>
    </Router>
  );
}

export default App;