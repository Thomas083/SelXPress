<template>
  <div :onChange="$emit('inputForm', formData)" class="form-container">
    <input-component
      label="Username"
      id="input-uname"
      name="input-uname"
      type="text"
      placeholder="Enter your username"
      class="form-input"
      @input="updateData($event, 'username')"
    />
    <input-component
      label="Email Address"
      id="input-mail"
      name="input-mail"
      type="email"
      placeholder="Enter your email"
      class="form-input"
      @input="updateData($event, 'email')"
    />
    <input-component
      label="Password"
      id="input-pwd"
      name="input-pwd"
      type="password"
      placeholder="Enter your password"
      class="form-input"
      @input="updateData($event, 'password')"
    />
    <input-component
      label="Confirm Password"
      id="input-confirm-pwd"
      name="input-confirm-pwd"
      type="password"
      placeholder="Confirm your password"
      class="form-input"
      @input="updateData($event, 'cpassword')"
    />
  </div>
</template>

<script>
import InputComponent from "@/components/global/InputComponent.vue";
export default {
  name: "RegisterForm",
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
        roleId: 1
      },
    };
  },
  methods: {
    updateData(e, key) {
      this.formData = Object.assign(this.formData, { [key]: e });
    },
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
