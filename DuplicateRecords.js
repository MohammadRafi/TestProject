// utils.js
export function getDuplicateRecordsByType(data) {
  if (!Array.isArray(data)) return [];

  // Count occurrences of each type
  const typeCount = data.reduce((acc, item) => {
    if (!item?.type) return acc; // safety check for null/undefined
    acc[item.type] = (acc[item.type] || 0) + 1;
    return acc;
  }, {});

  // Filter only duplicates (count > 1)
  const duplicates = data.filter((item, index, self) =>
    typeCount[item.type] > 1 &&
    index === self.findIndex(obj => obj.type === item.type) // keep only 1 copy
  );

  // Return only id & type
  return duplicates.map(item => ({
    id: item.id,
    type: item.type,
  }));
}
