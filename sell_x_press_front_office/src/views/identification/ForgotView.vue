<template>
  <div class="container">
    <div class="identification-container col-11 col-md-5">
      <h1>Forgot Password</h1>
      <input-component
        label="Email Address"
        id="input-email"
        name="input-email"
        type="email"
        placeholder="Enter your email"
        @input="updateData"
      />
      <a class="link-dark" href="/login">You already have an account ?</a>
      <button class="btn btn-primary send-button">
        SEND
        <img class="send-img" src="../../assets/Modal/send.png" />
      </button>
    </div>
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent.vue";
export default {
  name: "ForgotView",
  components: {
    InputComponent,
  },
  data() {
    return {
        email: "",
    };
  },
  methods: {
    updateData(e) {
      this.email = e;
    },
  },
};
</script>

<style lang="css" scoped>
.container {
  display: flex;
  justify-content: center;
  align-items: center;
  max-height: 80vh;
  width: 100%;
  padding-top: 5vh;
  padding-bottom: 5vh;
}

.identification-container {
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
  max-height: 70vh;
  min-height: 50vh;
  background-color: #ffffff;
  border-radius: 10px;
  padding-top: 3vh;
}

a {
  align-self: flex-end;
  margin-right: 2vw;
  margin-top: 10vh;
}

.send-img {
  width: 1.5rem;
}

.send-button {
  display: flex;
  align-self: flex-end;
  margin-right: 2vw;
  margin-bottom: 2vh;
  align-items: center;
  justify-content: center;
  gap: 1rem;
}
</style>
