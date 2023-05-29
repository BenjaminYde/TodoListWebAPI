import React, { useEffect, useState } from 'react';

function App() {
    const [textures, setTextures] = useState([]);

    useEffect(() => {
        fetch('http://0.0.0.0:80/textures')
            .then(response => response.json())
            .then(data => {
                console.log(data); // This will print the data to your console
                setTextures(data);
            })
            .catch(error => console.error('Error:', error));
    }, []);

    return (
        <div>
          <p>hello thereeee</p> {/* Corrected this line */}
          <div className="App">
              {textures.map(texture => (
                  <div key={texture.id}>
                      <h2>{texture.name}</h2>
                      <p>{texture.tags.join(', ')}</p>
                      <img src={texture.image} alt={texture.name} />
                  </div>
              ))}
          </div>
        </div>
      );      
}

export default App;
