import React from "react";
import { NavLink } from "react-router-dom";
import styles from "./NavBar.module.css";

const NavBar: React.FC = () => (
  <nav className={styles.navbar}>
    <NavLink
      to="/dashboard"
      className={({ isActive }) => isActive ? `${styles.navlink} ${styles.active}` : styles.navlink}
    >
      Dashboard
    </NavLink>
    <NavLink
      to="/plants"
      className={({ isActive }) => isActive ? `${styles.navlink} ${styles.active}` : styles.navlink}
    >
      My Plants
    </NavLink>
    <NavLink
      to="/add-plant"
      className={({ isActive }) => isActive ? `${styles.navlink} ${styles.active}` : styles.navlink}
    >
      Add Plant
    </NavLink>
  </nav>
);

export default NavBar;
