// ServicesPopup.js
import React, { useState } from "react";
import axios from "axios";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  FormGroup,
  FormControlLabel,
  Checkbox,
} from "@mui/material";

const ServicesPopup = () => {
  const [open, setOpen] = useState(false);
  const [selectedOptions, setSelectedOptions] = useState([]);

  const options = ["Water", "Sewer", "Trash"];

  const handleCheckboxChange = (option) => {
    setSelectedOptions((prev) =>
      prev.includes(option)
        ? prev.filter((item) => item !== option)
        : [...prev, option]
    );
  };

  const handleSubmit = async () => {
    try {
      const response = await axios.post("/api/services/submit", {
        services: selectedOptions,
      });

      alert("Submitted successfully: " + JSON.stringify(response.data));
      setOpen(false);
    } catch (error) {
      console.error("Error submitting services", error);
      alert("Something went wrong!");
    }
  };

  return (
    <div>
      {/* Button to open popup */}
      <Button variant="contained" color="primary" onClick={() => setOpen(true)}>
        Choose Services
      </Button>

      {/* Popup */}
      <Dialog open={open} onClose={() => setOpen(false)}>
        <DialogTitle>Select Services</DialogTitle>
        <DialogContent>
          <FormGroup>
            {options.map((option) => (
              <FormControlLabel
                key={option}
                control={
                  <Checkbox
                    checked={selectedOptions.includes(option)}
                    onChange={() => handleCheckboxChange(option)}
                  />
                }
                label={option}
              />
            ))}
          </FormGroup>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpen(false)} color="secondary">
            Cancel
          </Button>
          <Button onClick={handleSubmit} color="primary" variant="contained">
            OK
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
};

export default ServicesPopup;
