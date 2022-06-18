import "../App.css";
import { Box, Button, Collapse, IconButton, Table, TableBody, TableCell, TableHead, TableRow, Typography } from "@mui/material";
import Add from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import React, { useEffect, useState } from "react";
import { appState } from "../store/AppState";
import { Company } from "../services";

/**
 * An expandable row representing a Company in the UI.
 */
function CompanyRow(props: { company: Company }) {
  function reducer(a: number, b: number): number {
    return a + b;
  }

  const { company } = props;
  const [open, setOpen] = useState(false);
  const [loaded, setLoaded] = useState(false);

  // Trigger on the initial load.
  useEffect(() => {
    if (open && !loaded) {
      console.log(`Loading employees for company ${company.id}...`);

      (async () => await appState.loadEmployeesFor(company))()
        .catch(console.log);

      setLoaded(true);
    }
  });

  return (
    <React.Fragment>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton onClick={() => setOpen(!open)} >
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell><strong>{company.label}</strong> ${company.employees ? company.employees?.map(e => e.salary || 0).reduce(reducer, 0) : 0}</TableCell>
        <TableCell>{company.id}</TableCell>
        <TableCell>
          <IconButton onClick={async () => await appState.deleteCompany(company)}>
            <DeleteIcon titleAccess="Delete Company" />
          </IconButton>
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={4}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography variant="h6" gutterBottom component="div">
                Employees ({company.employees?.length})
                <Button
                  sx={{ ml: 4 }}
                  variant="outlined"
                  size="small"
                  startIcon={<Add />}
                  onClick={async () => await appState.addEmployeeTo(company)}
                  disabled={company.employees ? company.employees.length >= 10 : false}>
                  Add Employee
                </Button>
              </Typography>
              <Table size="small">
                <TableHead>
                  <TableRow>
                    <TableCell>Title</TableCell>
                    <TableCell>First Name</TableCell>
                    <TableCell>Last Name</TableCell>
                    <TableCell>Compensation</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {
                    company.employees?.map((employee) =>
                    (
                      <TableRow key={employee.id}>
                        <TableCell>{employee.label}</TableCell>
                        <TableCell>{employee.firstName}</TableCell>
                        <TableCell>{employee.lastName}</TableCell>
                        <TableCell>${employee.salary}</TableCell>
                      </TableRow>
                    ))
                  }
                </TableBody>
              </Table>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

export default CompanyRow;