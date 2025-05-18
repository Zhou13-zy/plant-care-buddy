import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { CareEvent } from '../../models/CareEvent/careEvent';
import { CareEventType } from '../../models/CareEvent/careEventType';
import { deleteCareEvent } from '../../api/careEventService';
import { getCareEventTypeName, getCareEventTypeClass } from '../../utils/careEventUtils';
import ImageDisplay from '../common/ImageDisplay';
import './CareEventList.css';

interface CareEventListProps {
  events: CareEvent[];
  plantId: number;
  onEventDeleted?: () => void;
  onAddEvent?: () => void;
}

const CareEventList: React.FC<CareEventListProps> = ({ 
  events, 
  plantId, 
  onEventDeleted, 
  onAddEvent 
}) => {
  const navigate = useNavigate();
  const [filter, setFilter] = useState<CareEventType | 'all'>('all');
  const [isDeleting, setIsDeleting] = useState<number | null>(null);

  // Filter events based on selected type
  const filteredEvents = filter === 'all' 
    ? events 
    : events.filter(event => event.eventType === filter);

  // Sort events by date (most recent first)
  const sortedEvents = [...filteredEvents].sort(
    (a, b) => new Date(b.eventDate).getTime() - new Date(a.eventDate).getTime()
  );

  const handleEdit = (eventId: number) => {
    navigate(`/care-events/${eventId}/edit`);
  };

  const handleDelete = async (eventId: number) => {
    if (window.confirm('Are you sure you want to delete this care event?')) {
      setIsDeleting(eventId);
      try {
        await deleteCareEvent(eventId);
        if (onEventDeleted) {
          onEventDeleted();
        }
      } catch (error) {
        console.error('Failed to delete care event:', error);
        alert('Failed to delete care event. Please try again.');
      } finally {
        setIsDeleting(null);
      }
    }
  };

  const handleAddEvent = () => {
    if (onAddEvent) {
      onAddEvent();
    } else {
      navigate(`/plants/${plantId}/care-events/add`);
    }
  };

  // Generate filter options from CareEventType enum
  const filterOptions = Object.entries(CareEventType)
    .filter(([key, value]) => typeof value === 'number')
    .map(([key, value]) => (
      <option key={value} value={value}>
        {key}
      </option>
    ));

  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
    });
  };

  return (
    <div className="care-event-list-container">
      <div className="care-event-header">
        <h3>Care History</h3>
        <button 
          className="add-event-button"
          onClick={handleAddEvent}
        >
          Add Care Event
        </button>
      </div>

      <div className="care-event-filters">
        <label htmlFor="event-filter">Filter by type:</label>
        <select 
          id="event-filter" 
          value={filter === 'all' ? 'all' : filter}
          onChange={(e) => setFilter(e.target.value === 'all' ? 'all' : Number(e.target.value) as CareEventType)}
        >
          <option value="all">All Events</option>
          {filterOptions}
        </select>
      </div>

      {sortedEvents.length === 0 ? (
        <div className="no-events">
          <p>No care events found. Start tracking your plant care by adding an event.</p>
        </div>
      ) : (
        <div className="care-event-list">
          {sortedEvents.map(event => (
            <div key={event.id} className="care-event-item">
              <div className={`event-type ${getCareEventTypeClass(event.eventType)}`}>
                {getCareEventTypeName(event.eventType)}
              </div>
              <div className="event-details">
                <div className="event-date">{formatDate(event.eventDate)}</div>
                {event.notes && <div className="event-notes">{event.notes}</div>}
                
                <div className="event-photos" style={{ 
                  display: 'flex', 
                  flexDirection: 'row',
                  justifyContent: 'flex-start', 
                  alignItems: 'flex-start',
                  gap: '20px',
                  marginTop: '10px'
                }}>
                  {event.beforeImagePath && (
                    <div className="event-photo">
                      <h4>Before:</h4>
                      <ImageDisplay 
                        imagePath={event.beforeImagePath} 
                        alt={`Before ${event.eventTypeName}`}
                        style={{ 
                          width: '150px', 
                          height: '120px', 
                          objectFit: 'cover',
                          borderRadius: '4px'
                        }}
                      />
                    </div>
                  )}
                  
                  {event.afterImagePath && (
                    <div className="event-photo">
                      <h4>After:</h4>
                      <ImageDisplay 
                        imagePath={event.afterImagePath} 
                        alt={`After ${event.eventTypeName}`}
                        style={{ 
                          width: '150px', 
                          height: '120px', 
                          objectFit: 'cover',
                          borderRadius: '4px'
                        }}
                      />
                    </div>
                  )}
                </div>
              </div>
              <div className="event-actions">
                <button 
                  className="edit-button" 
                  onClick={() => handleEdit(event.id)}
                >
                  Edit
                </button>
                <button 
                  className="delete-button" 
                  onClick={() => handleDelete(event.id)}
                  disabled={isDeleting === event.id}
                >
                  {isDeleting === event.id ? 'Deleting...' : 'Delete'}
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default CareEventList;