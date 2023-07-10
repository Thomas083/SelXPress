<template>
  <div :onChange="$emit('inputForm', formData)" class="form-container">
    <label for="input-uname">Username</label>
    <input
      id="input-uname"
      name="input-uname"
      type="text"
      placeholder="Enter your username"
      v-model="formData.username"
    />
    <label for="input-mail">Email Address</label>
    <input
      id="input-mail"
      name="input-mail"
      type="email"
      placeholder="Enter your email"
      v-model="formData.email"
    />
    <label for="input-pwd">Password</label>
    <input
      id="input-pwd"
      name="input-pwd"
      type="password"
      placeholder="Enter your password"
      v-model="formData.password"
    />
    <label for="input-confirm-pwd">Confirm Password</label>
    <input
      id="input-confirm-pwd"
      name="input-confirm-pwd"
      type="password"
      placeholder="Confirm your password"
      v-model="formData.cpassword"
    />
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent.vue";
export default {
  name: "LoginForm",
  components: {
    InputComponent,
  },
  data() {
    return {
      formData: {
        username: "",
        email: "",
        password: "",
        cpassword: "",
      },
    };
  },
};
</script>

<style scoped>
.form-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 75%;
  width: 100%;
  border-radius: 10px;
  margin-top: 5vh;
}

label {
  align-self: start;
  margin-left: 3vw;
}

input {
  border-radius: 15px;
  height: 53px;
  width: 90%;
  text-align: center;
}
</style>
