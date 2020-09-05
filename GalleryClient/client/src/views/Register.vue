<template>
  <div class="register">
    <h1>Register</h1>
    <input type="text" name="email" placeholder="Email">
    <input type="text" name="username" placeholder="Username">
    <input type="password" name="password" placeholder="Password">
    <input type="submit" value="Send" class="submit-btn">
  </div>  
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'Login' 
});
</script>

<style lang="scss">
    .register {
        * {
            display: block;
            margin: 1em auto;
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