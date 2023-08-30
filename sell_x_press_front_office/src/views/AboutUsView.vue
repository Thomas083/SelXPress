<template>
  <div class="about-us-container">
    <h1>About Us</h1>
    <p>
      We are a group of passionate students from Epitech Lille, working on our final year project.
      <br>Our team consists of 5 members with diverse skills, all contributing to create something amazing.
    </p>
    <div class="separation-line"></div>
    <h2>Our Team</h2>
    <ul class="team-members">
      <div>
        <h3><img src='../assets/AboutUs/front.png'> Front-End Developers</h3>
        <ul>
          <li><a href="https://www.linkedin.com/in/maxence-leroy-474489250/" target="_blank">Leroy Maxence</a></li>
          <li><a href="https://www.linkedin.com/in/julien-lamalle-/" target="_blank">Lamalle Julien</a></li>
        </ul>
      </div>
        <h3> <img src='../assets/AboutUs/back.png'> Back-End Developers</h3>
        <ul>
          <li><a href="https://www.linkedin.com/in/ugo-bertrand-624459214/" target="_blank">Bertrand Ugo</a></li>
          <li><a href="https://www.linkedin.com/in/thomas-debray-v/" target="_blank">Debray Thomas</a></li>
        </ul>
        <h3><img src='../assets/AboutUs/docker.png'> Docker Specialist</h3>
        <ul>
          <li><a href="https://www.linkedin.com/in/david-vacossin-fistache-08254620a/" target="_blank">Vacossin David</a></li>
        </ul>
    </ul>
    <div class="separation-line"></div>
    <h2>Stay tuned for more updates!</h2>
    <p>
      We are excited to showcase our skills and bring our project to life. Stay tuned for more updates!
    </p>
  </div>
</template>
  
<script>
export default {
  name: 'AboutUsView',
};
</script>
  
<style scoped>
.about-us-container {
  max-width: 800px;
  margin: 0 auto;
  padding: 2rem;
  background-color: var(--main-white);
  border-radius: 1rem;
  text-align: justify;
}

.separation-line {
  border: 1px var(--main-grey-separation) solid;
  width: 100%;
}

h1 {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

h2 {
  font-size: 2rem;
  margin-top: 2rem;
}

ul {
  list-style-type: disc;
  padding-left: 1.5rem;
}

li {
  margin-bottom: 0.5rem;
}

.team-members {
  margin-top: 1rem;
}

img {
  height: 8vh;
}
</style>
  