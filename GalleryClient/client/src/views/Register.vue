<template>
  <div class="register">
    <h1>Register</h1>
    <p class="error">{{error}}</p>
    <form method="post" @submit.prevent="handleSubmit">
      <input type="email" v-model="email" name="email" placeholder="Email" required />
      <input type="text" v-model="username" name="username" placeholder="Username" required />
      <input type="password" v-model="password" name="password" placeholder="Password" required />
      <input type="submit" value="Send" class="submit-btn" />
    </form>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { userService } from "../services";

export default defineComponent({
  name: "Register",
  data() {
    return {
      email: "",
      username: "",
      password: "",
      error: "",
    };
  },
  methods: {
    async handleSubmit() {
      const { email, username, password } = this;
      const isCorrect = this.validate();
      
      if (!isCorrect) {
        return;
      }
      var description = await userService.register(email, username, password);

      if (description !== null) {
        this.error = description;
      }
    },
    validate() {
      if (
        this.email.length === 0 ||
        this.username.length === 0 ||
        this.password.length === 0
      ) {
        this.error = "Invalid date!";
        return false;
      }
        
      if (this.email.length < 4) {
        this.error = "Invalid email!";
        return false;
      }

      if (this.password.length < 6) {
        this.error = "Invalid password!";
        return false;
      }

      return true;
    },
  },
});
</script>

<style lang="scss">
.register {
  * {
    display: block;
    margin: 1em auto;
  }

  .error {
    color: red;
  }

  input {
    width: 30em;
    height: 2em;
  }

  .submit-btn {
    border: none;
    width: 30em;
    background: #111111;
    color: #ffff;
  }
}
</style>