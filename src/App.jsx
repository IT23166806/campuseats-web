
import { useEffect, useState } from 'react'
import './App.css'

const API_URL = import.meta.env.VITE_API_URL

function App() {
  const [menu, setMenu] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() => {
    async function loadMenu() {
      try {
        if (!API_URL) {
          throw new Error('VITE_API_URL is not configured')
        }

        const response = await fetch(`${API_URL}/api/menu`)

        if (!response.ok) {
          throw new Error(`API returned status ${response.status}`)
        }

        const data = await response.json()
        setMenu(data)
      } catch (err) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }

    loadMenu()
  }, [])

  return (
    <div className="campuseats">
      <header className="header">
        <h1>CampusEats</h1>
        <p>Fresh and delicious campus meals</p>
      </header>

      <main className="content">
        <h2>Our Menu</h2>

        {loading && <p>Loading menu...</p>}

        {error && (
          <p className="error">
            Unable to load menu: {error}
          </p>
        )}

        {!loading && !error && menu.length === 0 && (
          <p>No menu items available.</p>
        )}

        <div className="menu-grid">
          {menu.map((item) => (
            <div className="menu-card" key={item.id}>
              <h3>{item.name}</h3>
              <p className="price">Rs. {item.price}</p>
              <span>Available on Campus</span>
            </div>
          ))}
        </div>
      </main>

      <footer>
        CampusEats © 2026
      </footer>
    </div>
  )
}

export default App
