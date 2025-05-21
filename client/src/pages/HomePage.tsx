import React from 'react';
import { Link } from 'react-router-dom';
import './HomePage.css';

const HomePage: React.FC = () => {
  return (
    <div className="home-container">
      <h1>Welcome to Plant Buddy</h1>
      <p className="home-description">
        Your digital companion for tracking, caring, and celebrating your houseplants.
      </p>
      <div className="home-actions">
        <Link to="/plants" className="home-btn">View My Plants</Link>
        <Link to="/add-plant" className="home-btn primary">Add a New Plant</Link>
      </div>
    </div>
  );
};

export default HomePage;