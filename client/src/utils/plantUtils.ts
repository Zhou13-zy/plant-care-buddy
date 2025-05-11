export const healthStatusMap: { [key: string]: number } = {
  'Healthy': 0,
  'NeedsAttention': 1,
  'Unhealthy': 2,
  'Dormant': 3
};

export const healthStatusOptions = [
  { value: 0, label: 'Healthy' },
  { value: 1, label: 'NeedsAttention' },
  { value: 2, label: 'Unhealthy' },
  { value: 3, label: 'Dormant' },
]; 