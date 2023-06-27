<template>
  <div class="about">
    <h1>This is an about page</h1>
    <div class="maxence">
      <p class="maxence-p">
        COUCOU MAXENCE ! 
      </p>
    </div>
  </div>
</template>

<script>
export default {

}
</script>

<style>
  .about {
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  .maxence {
    background-color: pink;
    border-radius: 20px;
    height: 30%;
    width: 30%;
  }
  .maxence-p {
    font-size: 30px;
  }
</style>